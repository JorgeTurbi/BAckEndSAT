using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Vacante
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }
    // Relaciones
    [ForeignKey("Institucion")]
    public int InstitucionId { get; set; }

    [ForeignKey("Provincia")]
    public int ProvinciaId { get; set; }

    [ForeignKey("Categoria")]
    public int CategoriaId { get; set; }

    // Campos principales
    [Required, MaxLength(150)]
    public required string Titulo { get; set; }

    [Required]
    public TipoContrato TipoContrato { get; set; }

    [MaxLength(200)]
    public string? SalarioCompensacion { get; set; } // libre, ej: "RD$85,000 - RD$120,000 mensual"

    public DateTime? FechaLimiteAplicacion { get; set; }

    [MaxLength(120)]
    public string? HorarioTrabajo { get; set; } // ej: "Lunes a Viernes 8:00 AM - 5:00 PM"

    [MaxLength(120)]
    public string? DuracionContrato { get; set; } // ej: "Indefinido, 1 año, 2 años"

    // Textos largos
    [Required]
    public required string DescripcionPuesto { get; set; }

    public string? ResponsabilidadesEspecificas { get; set; }

    public required string RequisitosGenerales { get; set; }

    public string? EducacionRequerida { get; set; }
    public string? ExperienciaRequerida { get; set; }
    public string? HabilidadesCompetencias { get; set; }
    public string? BeneficiosCompensaciones { get; set; }

    [Required, MaxLength(300)]
    public required string Telefono { get; set; }
    public required string Email { get; set; }
       public string? Direccion { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navegación
    public virtual Institucion? Institucion { get; set; }
    public virtual Provincia? Provincia { get; set; }
    public virtual CategoriaVacante? Categoria { get; set; }
    public virtual User? User { get; set; }
}
