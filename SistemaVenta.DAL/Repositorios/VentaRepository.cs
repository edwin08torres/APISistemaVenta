﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.MODEL;

namespace SistemaVenta.DAL.Repositorios
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private DbventaContext _dbcontext;

        public VentaRepository(DbventaContext dbcontext):base(dbcontext) 
        {
            _dbcontext = dbcontext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();

            using( var transaction = _dbcontext.Database.BeginTransaction() ) {
                try
                {
                    foreach(DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Producto producto_encontrado = _dbcontext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        _dbcontext.Productos.Update(producto_encontrado);

                        Cliente clienteAsociado = _dbcontext.Clientes.FirstOrDefault(c => c.Idcliente == dv.Idcliente);

                        if (clienteAsociado != null)
                        {
                            DetalleVentaDTO detalleVentaDTO = new DetalleVentaDTO()
                            {
                                IdProducto = dv.IdProducto,
                                DescripcionProducto = dv.IdProductoNavigation?.Nombre,
                                Cantidad = dv.Cantidad,
                                PrecioTexto = dv.Precio?.ToString(),
                                TotalTexto = dv.Total?.ToString(),
                                IdCliente = clienteAsociado.Idcliente,
                                Cedula = clienteAsociado.Cedula,
                                NombreCliente = clienteAsociado.NombreCliente
                            };
                        }
                    }   

                    await _dbcontext.SaveChangesAsync();

                    NumeroDocumento correlativo = _dbcontext.NumeroDocumentos.First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbcontext.NumeroDocumentos.Update(correlativo);
                    await _dbcontext.SaveChangesAsync();

                    int cantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", cantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();

                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - cantidadDigitos, cantidadDigitos);

                    modelo.NumeroDocumento =  numeroVenta;

                    await _dbcontext.Venta.AddAsync(modelo);
                    await _dbcontext.SaveChangesAsync();

                    ventaGenerada = modelo;

                    transaction.Commit();

                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                return ventaGenerada;
            }
        }
    }
}
