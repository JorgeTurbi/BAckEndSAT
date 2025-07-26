using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Provincia
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public required string Nombre { get; set; }

    public virtual ICollection<Vacante> Vacantes { get; set; } = new List<Vacante>();
}
