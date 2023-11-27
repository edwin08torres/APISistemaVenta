using AutoMapper;
using Azure.Core.Pipeline;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Servicios
{
    public class ProveedorService : IProveedor
    {
        private readonly IGenericRepository<Proveedore> _proveedorRepositorio;
        private readonly IMapper _mapper;

        public ProveedorService(IGenericRepository<Proveedore> proveedorRepositorio, IMapper mapper)
        {
            _proveedorRepositorio = proveedorRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ProveedorDTO>> listar()
        {
            try
            {

                var listaProveedores = await _proveedorRepositorio.Consultar();
                return _mapper.Map<List<ProveedorDTO>>(listaProveedores.ToList());

            }
            catch
            {
                throw;
            }
        }

       public async Task<ProveedorDTO>Crear (ProveedorDTO modelo)
        {
            try
            {
                var proveedorcreacion = await _proveedorRepositorio.Crear(_mapper.Map<Proveedore>(modelo));

                if (proveedorcreacion.Idproveedor == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _mapper.Map<ProveedorDTO>(proveedorcreacion);

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ProveedorDTO modelo)
        {
            try
            {

                var proveedorModelo = _mapper.Map<Proveedore>(modelo);
                var proveedorEncontrado = await _proveedorRepositorio.Obtener(u =>
                    u.Idproveedor == proveedorModelo.Idproveedor
                );

                if (proveedorEncontrado == null)
                    throw new TaskCanceledException("El proveedor no existe");


                proveedorEncontrado.Telefono = proveedorModelo.Telefono;
                proveedorEncontrado.Direccion = proveedorModelo.Direccion;
                proveedorEncontrado.Activo = proveedorModelo.Activo;

                bool respuesta = await _proveedorRepositorio.Editar(proveedorEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar"); ;


                return respuesta;



            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {

                var proveedorEncontrado = await _proveedorRepositorio.Obtener(p => p.Idproveedor == id);

                if (proveedorEncontrado == null)
                    throw new TaskCanceledException("El proveedor no existe");

                bool respuesta = await _proveedorRepositorio.Eliminar(proveedorEncontrado);


                if (!respuesta)
                    throw new TaskCanceledException("No se pudo elminar"); ;

                return respuesta;

            }
            catch
            {
                throw;
            }
        }
    }
}
