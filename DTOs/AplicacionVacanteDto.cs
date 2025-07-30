using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs;

public class AplicacionVacanteDto
{    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int AplicanteId { get; set; }  
    public int VacanteId { get; set; } 
    public int EstadoId { get; set; }  




}
