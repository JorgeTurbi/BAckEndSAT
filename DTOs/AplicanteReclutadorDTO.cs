// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs;

public class AplicanteReclutadorDTO
{
    // [Key]
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    // public int Id { get; set; }  // Clave primaria de la entidad                                 
    public int AplicanteId { get; set; }
    public int UserId { get; set; }
    public string? ProfileImage { get; set; }  // Base64 desde Angular
    public string? Name { get; set; }
    public string? Rango { get; set; }
    public string? Specialization { get; set; }
    public int EstadoId { get; set; }
    public string? Estado { get; set; }
    public DateTime FechaAplicacion { get; set; }
    public double MatchPorcentaje { get; set; }


}