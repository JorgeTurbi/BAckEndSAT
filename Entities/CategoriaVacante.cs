using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class CategoriaVacante
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public required string Nombre { get; set; }

    [MaxLength(400)]
    public string? Descripcion { get; set; }

    public virtual ICollection<Vacante> Vacantes { get; set; } = new List<Vacante>();
}
