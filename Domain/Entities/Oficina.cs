using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Oficina
{
    public string CodigoOficina { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string? Region { get; set; }

    public string CodigoPostal { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string LineaDireccion1 { get; set; } = null!;

    public string? LineaDireccion2 { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
