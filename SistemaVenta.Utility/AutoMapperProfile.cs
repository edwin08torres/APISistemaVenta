﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SistemaVenta.DTO;
using SistemaVenta.MODEL;

namespace SistemaVenta.UTILITY
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion

            #region Cliente
            CreateMap<Cliente, ClienteDTO>()
                 .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.Activo == true ? 1 : 0)
                );

            CreateMap<ClienteDTO, Cliente>()
                 .ForMember(destino =>
                     destino.Activo,
                     opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                 );

            #endregion

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion

            #region usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino =>
                    destino.RolDescripcion,
                    opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                 )
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                 );

            CreateMap<Usuario, SesionDTO>()
               .ForMember(destino =>
                   destino.RolDescripcion,
                   opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                );

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino =>
                    destino.IdRolNavigation,
                    opt => opt.Ignore()
                 )
                  .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                 );
            #endregion

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion

            #region Producto
            CreateMap<Producto, ProductoDTO>()
                .ForMember(destino =>
                    destino.DescripcionCategoria,
                    opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre)
                  )
                  .ForMember(destino =>
                    destino.nombreProveedor,
                    opt => opt.MapFrom(origen => origen.IdproveedorNavigation.NombreProveedor)
                )
                .ForMember(destino =>
                    destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-NIC")))
                  )
                 .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                 );

            CreateMap<ProductoDTO, Producto>()
             .ForMember(destino =>
                  destino.IdCategoriaNavigation,
                  opt => opt.Ignore()
                 )
                 .ForMember(destino =>
                   destino.IdproveedorNavigation,
                   opt => opt.Ignore()
               )
                .ForMember(destino =>
                     destino.Precio,
                     opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-NIC")))
                 )
             .ForMember(destino =>
                 destino.EsActivo,
                     opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );
            #endregion

            #region Venta
            CreateMap<Venta, VentaDTO>()
                 .ForMember(destino =>
                    destino.TotalTexto,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-NIC")))
                 )
                  .ForMember(destino =>
                    destino.FechaRegistro,
                    opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                 );

            CreateMap<VentaDTO, Venta>()
                .ForMember(destino =>
                   destino.Total,
                   opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-NIC")))
                );
            #endregion

            #region Proveedor
            CreateMap<Proveedore, ProveedorDTO>()
                .ForMember(destino =>
                          destino.FechaRegistro,
                          opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                        )
                 .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.Activo == true ? 1 : 0)
                );
            CreateMap<ProveedorDTO, Proveedore>()
                .ForMember(destino =>
                   destino.Activo,
                   opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
               );
            #endregion

            #region DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDTO>()
             .ForMember(destino =>
                    destino.DescripcionProducto,
                    opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                  )
                .ForMember(destino =>
                    destino.Cedula,
                    opt => opt.MapFrom(origen => origen.IdclienteNavigation.Cedula)
                )
                .ForMember(destino =>
                    destino.NombreCliente,
                    opt => opt.MapFrom(origen => origen.IdclienteNavigation.NombreCliente)
                )
              .ForMember(destino =>
                    destino.PrecioTexto,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-NIC")))
                  )

              .ForMember(destino =>
                    destino.TotalTexto,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-NIC")))
                  );

            CreateMap<DetalleVentaDTO, DetalleVenta>()
            .ForMember(destino =>
                 destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("es-NIC")))
                   )

          .ForMember(destino =>
                 destino.Total,
                     opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-NIC")))
                  );
            #endregion

            #region Reporte
            CreateMap<DetalleVenta, ReporteDTO>()
                .ForMember(destino =>
                    destino.FechaRegistro,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                 )
                .ForMember(destino =>
                    destino.NumeroDocumento,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento)
                 )
                .ForMember(destino =>
                    destino.TipoPago,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.TipoPago)
                 )
                  .ForMember(destino =>
                    destino.TotalVenta,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-NIC")))
                  )
                 .ForMember(destino =>
                    destino.Producto,
                    opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                  )
                 .ForMember(destino =>
                    destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-NIC")))
                  ).ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-NIC")))
                  );


            #endregion
        }
    }
}
