using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Rango
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public int InstitucionMilitarId { get; set; }
    public required string Nombre { get; set; }

    public virtual InstitucionMilitar? InstitucionMilitar { get; set; }
}