using System.ComponentModel.DataAnnotations;

namespace API.Dtos;
public class RegisterDto
{
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Email { get; set; }
}
