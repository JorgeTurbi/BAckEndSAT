using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs;

public class DepartamentoDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public string? Direccion { get; set; }
    public required string Director { get; set; }
}
