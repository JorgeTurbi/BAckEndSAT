using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class InstitucionMilitar
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public required string Institucion { get; set; }
    public virtual ICollection<Rango>? Rangos { get; set; }
}