using System.ComponentModel.DataAnnotations;

namespace DTOs;

public class UserProfileDto
{
    [Key]
    public int Id { get; set; }
    public string? Nombres { get; set; }
    public string? Apellidos { get; set; }
    public string? Usuario { get; set; }    
    public required string UserType { get; set; }
    public string? Email { get; set; }
    public string? Cedula { get; set; }
    public string? Role { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    // Información de la institución
    public int InstitutionId { get; set; }
    public string? InstitucionNombre { get; set; }
    public string? InstitucionCodigo { get; set; }

    // Información del departamento
    public int DepartamentoId { get; set; }
    public string? DepartamentoDireccion { get; set; }
    public string? DepartamentoDirector { get; set; }
}
