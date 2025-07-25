using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs;

public class UserDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public int InstitutionId { get; set; }
    public int DepartamentoId { get; set; }
    public required string Nombres { get; set; }
    public required string Apellidos { get; set; }
    public required string Usuario { get; set; }
    public required string UserType { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string Cedula { get; set; }
}
