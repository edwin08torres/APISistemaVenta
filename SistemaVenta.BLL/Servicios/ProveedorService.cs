using AutoMapper;
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
    }
}
