using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Education
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(150)]
    public string? Institution { get; set; }
    [MaxLength(100)]
    public string? Degree { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }

    // FK con UserProfile
    public int AplicanteId { get; set; }
    [ForeignKey(nameof(AplicanteId))]
    public Aplicante Aplicante { get; set; } = null!;
}