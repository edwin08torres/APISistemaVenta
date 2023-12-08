using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.MODEL
{
    public partial class Proveedore
    {
        public int Idproveedor { get; set; }

        public string NombreProveedor { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string? Direccion { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public bool? Activo { get; set; }

        public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
    }

}
