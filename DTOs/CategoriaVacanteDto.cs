using System.ComponentModel.DataAnnotations;

namespace DTOs;

public class CategoriaVacanteDto
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }
}
