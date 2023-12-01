﻿using SistemaVenta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IProveedor
    {
        Task<List<ProveedorDTO>> listar();
        Task<ProveedorDTO> Crear(ProveedorDTO modelo);
        Task<bool> Editar(ProveedorDTO modelo);
        Task<bool> Eliminar(int id);
    }
}
