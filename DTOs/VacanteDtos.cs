using System.ComponentModel.DataAnnotations;
using Entities;

namespace DTOs;

public class VacanteCreateDto
{
    [Required]
    public int InstitucionId { get; set; }

    [Required]
    public int ProvinciaId { get; set; }

    [Required]
    public int CategoriaId { get; set; }

    [Required]
    public string Titulo { get; set; } = null!;

    [Required]
    public TipoContrato TipoContrato { get; set; }

    public string? SalarioCompensacion { get; set; }
    public DateTime? FechaLimiteAplicacion { get; set; }
    public string? HorarioTrabajo { get; set; }
    public string? DuracionContrato { get; set; }

    [Required]
    public string DescripcionPuesto { get; set; } = null!;

    [Required]
    public string RequisitosGenerales { get; set; } = null!;
    public string? ResponsabilidadesEspecificas { get; set; }
    public string? EducacionRequerida { get; set; }
    public string? ExperienciaRequerida { get; set; }
    public string? HabilidadesCompetencias { get; set; }
    public string? BeneficiosCompensaciones { get; set; }

    [Required]
    public string InformacionContacto { get; set; } = null!;
    public bool IsActive { get; set; } = true;
}

public class VacanteUpdateDto : VacanteCreateDto
{
    [Required]
    public int Id { get; set; }
}

public class VacanteDto
{
    public int Id { get; set; }
    public string? Titulo { get; set; }
    public TipoContrato TipoContrato { get; set; }
    public string? SalarioCompensacion { get; set; }
    public DateTime? FechaLimiteAplicacion { get; set; }
    public string? HorarioTrabajo { get; set; }
    public string? DuracionContrato { get; set; }
    public string? DescripcionPuesto { get; set; }
    public string? ResponsabilidadesEspecificas { get; set; }
    public string? RequisitosGenerales { get; set; }
    public string? EducacionRequerida { get; set; }
    public string? ExperienciaRequerida { get; set; }
    public string? HabilidadesCompetencias { get; set; }
    public string? BeneficiosCompensaciones { get; set; }
    public string? InformacionContacto { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int InstitucionId { get; set; }
    public string? InstitucionNombre { get; set; }

    public int ProvinciaId { get; set; }
    public string? ProvinciaNombre { get; set; }

    public int CategoriaId { get; set; }
    public string? CategoriaNombre { get; set; }
}
