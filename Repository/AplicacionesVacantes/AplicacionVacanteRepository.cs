using AutoMapper;
using Context;
using DTOs;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.AplicacionesVacante;

namespace Repository.AplicacionesVacantes;

public class AplicacionVacanteRepository : IAplicacionVacanteService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public AplicacionVacanteRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GenericResponseDto<bool>> CrearAsync(AplicacionVacanteDto aplicar)
    {
        // Verificar si existe aplicante y vacante
        var aplicante = await _context.Aplicantes.FindAsync(aplicar.AplicanteId);
        var vacante = await _context.Vacantes.FindAsync(aplicar.VacanteId);


        if (aplicante == null || vacante == null)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Message = "Aplicante o Vacante no encontrados",
                Data = false
            };
        }
        var aplicaciones = await _context.AplicacionVacantes.Where(a => a.AplicanteId == aplicante.Id && a.VacanteId == vacante.Id).FirstOrDefaultAsync();


        if (aplicaciones != null)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Message = "Ya aplicates a esta vacante",
                Data = false
            };
        }

        // Calcular match
        double match = CalcularMatch(aplicante, vacante);

        var aplicacion = new AplicacionVacante
        {
            AplicanteId = aplicar.AplicanteId,
            VacanteId = aplicar.VacanteId,
            EstadoId = 1, // Pendiente por defecto
            Observaciones = "",
            MatchPorcentaje = match,
            FechaAplicacion = DateTime.UtcNow
        };

        await _context.AplicacionVacantes.AddAsync(aplicacion);
        bool result = await _context.SaveChangesAsync() > 0 ? true : false;
     

            return new GenericResponseDto<bool>
            {
                Success = result,
                Message = result==false? "ha habido un problema con la aplicacion a de la vacante":"Aplicación recibida, en espera de revisión",
                Data = result
            };
        

    
    }


    // Método privado para calcular el porcentaje de match
    private double CalcularMatch(Aplicante aplicante, Vacante vacante)
    {
        double totalCriteria = 0;
        double matchedCriteria = 0;

        // 1️⃣ Habilidades (Skills)
        if (!string.IsNullOrWhiteSpace(vacante.HabilidadesCompetencias))
        {
            totalCriteria++;

            var skillsVacante = vacante.HabilidadesCompetencias
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var skillsAplicante = aplicante.Skills?
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                ?? Array.Empty<string>();

            var skillMatches = skillsVacante.Count(skill =>
                skillsAplicante.Contains(skill, StringComparer.OrdinalIgnoreCase));

            if (skillMatches > 0) matchedCriteria++;
        }

        // 2️⃣ Experiencia (Positions)
        if (!string.IsNullOrWhiteSpace(vacante.ExperienciaRequerida))
        {
            totalCriteria++;

            var experienciaMatches = aplicante.Experience.Any(exp =>
                !string.IsNullOrWhiteSpace(exp.Position) &&
                vacante.ExperienciaRequerida.Contains(exp.Position, StringComparison.OrdinalIgnoreCase));

            if (experienciaMatches) matchedCriteria++;
        }

        // 3️⃣ Educación (Degree)
        if (!string.IsNullOrWhiteSpace(vacante.EducacionRequerida))
        {
            totalCriteria++;

            var educationMatches = aplicante.Education.Any(edu =>
                !string.IsNullOrWhiteSpace(edu.Degree) &&
                vacante.EducacionRequerida.Contains(edu.Degree, StringComparison.OrdinalIgnoreCase));

            if (educationMatches) matchedCriteria++;
        }

        // Evitar división por cero
        if (totalCriteria == 0) return 0;

        // Calcular porcentaje de match
        return Math.Round((matchedCriteria / totalCriteria) * 100, 2);
    }


    // public async Task<List<AplicacionVacanteDto>> GetAllAsync()
    // {
    //     throw new NotImplementedException();
    // }

    // public async Task<AplicacionVacanteDto?> GetByIdAsync(int aplicacionId)
    // {
    //     throw new NotImplementedException();
    // }



}