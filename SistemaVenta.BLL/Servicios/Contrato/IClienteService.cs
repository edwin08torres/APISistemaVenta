﻿using SistemaVenta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IClienteService
    {
        Task<List<ClienteDTO>> lista();
        Task<ClienteDTO> Crear(ClienteDTO modelo);
        Task<bool> Editar(ClienteDTO modelo);
        Task<bool> Eliminar(int id);
    }
}

