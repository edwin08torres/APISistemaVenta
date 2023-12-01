using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class ClienteDTO
    {
        public int Idcliente { get; set; }

        public string NombreCliente { get; set; }

        public string Cedula { get; set; }

        public string Direccion { get; set; } 

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public int? EsActivo { get; set; }
    }
}
