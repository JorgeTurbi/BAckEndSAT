using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [ForeignKey("Institucion")]
    public int InstitutionId { get; set; }

    [ForeignKey("Departamento")]
    public int DepartamentoId { get; set; }
    public required string Nombres { get; set; }
    public required string Apellidos { get; set; }
    public required string Usuario { get; set; }
    public required string UserType { get; set; }
    public required string Email { get; set; }
    public required string Cedula { get; set; }
    public required string PasswordHash { get; set; }
    public required string Role { get; set; }
    public bool IsActive { get; set; }
    public required DateTime CreatedAt { get; set; }

    public virtual Institucion? Institucion { get; set; }
    public virtual Departamento? Departamento { get; set; }

    // Nueva relación con Sessions
    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
