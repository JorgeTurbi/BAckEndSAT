using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Departamento
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public required string Direccion { get; set; }
    public required string Director { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

