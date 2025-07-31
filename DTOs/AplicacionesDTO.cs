using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndSAT.DTOs;

public class AplicacionesDTO
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required int  VacanteId { get; set; }
    public required string TituloVacante { get; set; } = string.Empty;
    public required string Departamento { get; set; }=string.Empty;
    public required string Ubicacion { get; set; }=string.Empty;
    public DateTime FechaAplicacion { get; set; }
    public required string Estado { get; set; }=string.Empty;
    public required int EstadoId { get; set; }
    public DateTime? Fechaentrevista { get; set; } = null;
    public string? Observaciones { get; set; }
    public double MatchPorcentaje { get; set; }


}
