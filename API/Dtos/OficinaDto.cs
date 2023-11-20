using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class OficinaDto
{
    public string CodigoOficina { get; set; } = null!;
    public string Ciudad { get; set; } = null!;
    public string Pais { get; set; } = null!;
    public string? Region { get; set; }
    public string CodigoPostal { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string LineaDireccion1 { get; set; } = null!;
    public string? LineaDireccion2 { get; set; }
}