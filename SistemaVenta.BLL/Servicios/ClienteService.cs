using AutoMapper;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Servicios
{
    public class ClienteService : IClienteService
    {
        private readonly IGenericRepository<Cliente> _clienteRepositorio;
        private readonly IMapper _mapper;

        public ClienteService(IGenericRepository<Cliente> clienteRepositorio, IMapper mapper)
        {
            _clienteRepositorio = clienteRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ClienteDTO>> lista()
        {
            try
            {
                var listaCliente = await _clienteRepositorio.Consultar();
                return _mapper.Map<List<ClienteDTO>>(listaCliente.ToList());
            }
            catch
            {
                throw;
            }
        }

        public async Task<ClienteDTO> Crear(ClienteDTO modelo)
        {
            try
            {
                var clientecreacion = await _clienteRepositorio.Crear(_mapper.Map<Cliente>(modelo));

                if (clientecreacion.Idcliente == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _mapper.Map<ClienteDTO>(clientecreacion);

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ClienteDTO modelo)
        {
            try
            {

                var clientemodelo = _mapper.Map<Cliente>(modelo);
                var clienteEncontrado = await _clienteRepositorio.obtener(u =>
                    u.Idcliente == clientemodelo.Idcliente
                );

                if (clienteEncontrado == null)
                    throw new TaskCanceledException("El cliente no existe");


                clienteEncontrado.NombreCliente = clientemodelo.NombreCliente;
                clienteEncontrado.Telefono = clientemodelo.Telefono;
                clienteEncontrado.Correo = clientemodelo.Correo;
                clienteEncontrado.Direccion = clientemodelo.Direccion;
                clienteEncontrado.Activo = clientemodelo.Activo;

                bool respuesta = await _clienteRepositorio.Editar(clienteEncontrado);

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

                var clienteEncontrado = await _clienteRepositorio.obtener(p => p.Idcliente == id);

                if (clienteEncontrado == null)
                    throw new TaskCanceledException("El cliente no existe");

                bool respuesta = await _clienteRepositorio.Delete(clienteEncontrado);


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
