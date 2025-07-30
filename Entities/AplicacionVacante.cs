
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class AplicacionVacante
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int AplicanteId { get; set; }
   public int VacanteId { get; set; }

    public DateTime FechaAplicacion { get; set; } = DateTime.UtcNow;    // FK Estado
    public int EstadoId { get; set; }


    [Range(0, 100)]
    public double MatchPorcentaje { get; set; }

    public string? Observaciones { get; set; }

    public virtual Aplicante? Aplicante { get; set; }
    public virtual Vacante? Vacante { get; set; }
    public virtual Estado? Estado { get; set; }
}
