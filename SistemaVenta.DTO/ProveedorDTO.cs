using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class ProveedorDTO
    {
        public int Idproveedor { get; set; }

        public string NombreProveedor { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string? Direccion { get; set; }

        public string? FechaRegistro { get; set; }
    }
}
