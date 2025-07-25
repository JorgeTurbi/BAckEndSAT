using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Institucion
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public string? UrlLogo { get; set; }
    public string? Nombre { get; set; }
    public required string CodigoNombre { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Direccion { get; set; }
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
