using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class Proveedore
{
    public int Idproveedor { get; set; }

    public string NombreProveedor { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Direccion { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

}
