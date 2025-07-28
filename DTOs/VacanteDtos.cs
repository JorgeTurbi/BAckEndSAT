using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities;

namespace DTOs;

public class VacanteCreateDto
{
    public int UserId { get; set; }

    public required int InstitucionId { get; set; }

    public required int ProvinciaId { get; set; }


    public required int CategoriaId { get; set; }


    public required string Titulo { get; set; } = null!;


    public required TipoContrato TipoContrato { get; set; }

    public string? SalarioCompensacion { get; set; }
    public DateTime? FechaLimiteAplicacion { get; set; }
    public string? HorarioTrabajo { get; set; }
    public string? DuracionContrato { get; set; }

    public required string DescripcionPuesto { get; set; } = null!;


    public required string RequisitosGenerales { get; set; } = null!;
    public string? ResponsabilidadesEspecificas { get; set; }
    public string? EducacionRequerida { get; set; }
    public string? ExperienciaRequerida { get; set; }
    public string? HabilidadesCompetencias { get; set; }
    public string? BeneficiosCompensaciones { get; set; }

    public required string InformacionContacto { get; set; } = null!;
    public required bool IsActive { get; set; } = true;
}

public class VacanteUpdateDto : VacanteCreateDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
}

public class VacanteDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }

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
