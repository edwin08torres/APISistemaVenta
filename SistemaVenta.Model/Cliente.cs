using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class Cliente
{
    public int Idcliente { get; set; }

    public string NombreCliente { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; } = new List<DetalleVenta>();
}
