using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public partial class Empleado
{
    public int CodigoEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidol { get; set; } = null!;

    public string? Apellido2 { get; set; }

    public string Extension { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string CodigoOficina { get; set; } = null!;

    public int? CodigoJefe { get; set; }

    public string? Puesto { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    [JsonIgnore]
    public virtual Empleado? CodigoJefeNavigation { get; set; }

    public virtual Oficina CodigoOficinaNavigation { get; set; } = null!;

    public virtual ICollection<Empleado> InverseCodigoJefeNavigation { get; set; } = new List<Empleado>();
}
