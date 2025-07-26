using Context;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class VacanteRepository : IVacanteRepository
{
    private readonly ApplicationDbContext _context;

    public VacanteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Vacante>> GetAllWithDetailsAsync()
    {
        // ✅ JOIN EXPLÍCITO sin Include
        return await (
            from v in _context.Vacantes
            join i in _context.Instituciones on v.InstitucionId equals i.Id
            join p in _context.Provincias on v.ProvinciaId equals p.Id
            join c in _context.CategoriasVacante on v.CategoriaId equals c.Id
            orderby v.CreatedAt descending
            select new Vacante
            {
                Id = v.Id,
                InstitucionId = v.InstitucionId,
                ProvinciaId = v.ProvinciaId,
                CategoriaId = v.CategoriaId,
                Titulo = v.Titulo,
                TipoContrato = v.TipoContrato,
                SalarioCompensacion = v.SalarioCompensacion,
                FechaLimiteAplicacion = v.FechaLimiteAplicacion,
                HorarioTrabajo = v.HorarioTrabajo,
                DuracionContrato = v.DuracionContrato,
                DescripcionPuesto = v.DescripcionPuesto,
                ResponsabilidadesEspecificas = v.ResponsabilidadesEspecificas,
                RequisitosGenerales = v.RequisitosGenerales,
                EducacionRequerida = v.EducacionRequerida,
                ExperienciaRequerida = v.ExperienciaRequerida,
                HabilidadesCompetencias = v.HabilidadesCompetencias,
                BeneficiosCompensaciones = v.BeneficiosCompensaciones,
                InformacionContacto = v.InformacionContacto,
                IsActive = v.IsActive,
                CreatedAt = v.CreatedAt,
                UpdatedAt = v.UpdatedAt,
                Institucion = new Institucion
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    CodigoNombre = i.CodigoNombre,
                },
                Provincia = new Provincia { Id = p.Id, Nombre = p.Nombre },
                Categoria = new CategoriaVacante { Id = c.Id, Nombre = c.Nombre },
            }
        ).ToListAsync();
    }

    public async Task<List<Vacante>> GetActiveWithDetailsAsync()
    {
        var now = DateTime.UtcNow;

        return await (
            from v in _context.Vacantes
            join i in _context.Instituciones on v.InstitucionId equals i.Id
            join p in _context.Provincias on v.ProvinciaId equals p.Id
            join c in _context.CategoriasVacante on v.CategoriaId equals c.Id
            where
                v.IsActive && (!v.FechaLimiteAplicacion.HasValue || v.FechaLimiteAplicacion >= now)
            orderby v.CreatedAt descending
            select new Vacante
            {
                Id = v.Id,
                InstitucionId = v.InstitucionId,
                ProvinciaId = v.ProvinciaId,
                CategoriaId = v.CategoriaId,
                Titulo = v.Titulo,
                TipoContrato = v.TipoContrato,
                SalarioCompensacion = v.SalarioCompensacion,
                FechaLimiteAplicacion = v.FechaLimiteAplicacion,
                HorarioTrabajo = v.HorarioTrabajo,
                DuracionContrato = v.DuracionContrato,
                DescripcionPuesto = v.DescripcionPuesto,
                ResponsabilidadesEspecificas = v.ResponsabilidadesEspecificas,
                RequisitosGenerales = v.RequisitosGenerales,
                EducacionRequerida = v.EducacionRequerida,
                ExperienciaRequerida = v.ExperienciaRequerida,
                HabilidadesCompetencias = v.HabilidadesCompetencias,
                BeneficiosCompensaciones = v.BeneficiosCompensaciones,
                InformacionContacto = v.InformacionContacto,
                IsActive = v.IsActive,
                CreatedAt = v.CreatedAt,
                UpdatedAt = v.UpdatedAt,
                Institucion = new Institucion
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    CodigoNombre = i.CodigoNombre,
                },
                Provincia = new Provincia { Id = p.Id, Nombre = p.Nombre },
                Categoria = new CategoriaVacante { Id = c.Id, Nombre = c.Nombre },
            }
        ).ToListAsync();
    }

    public async Task<Vacante?> GetByIdAsync(int id)
    {
        return await _context.Vacantes.FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<Vacante?> GetByIdWithDetailsAsync(int id)
    {
        return await (
            from v in _context.Vacantes
            join i in _context.Instituciones on v.InstitucionId equals i.Id
            join p in _context.Provincias on v.ProvinciaId equals p.Id
            join c in _context.CategoriasVacante on v.CategoriaId equals c.Id
            where v.Id == id
            select new Vacante
            {
                Id = v.Id,
                InstitucionId = v.InstitucionId,
                ProvinciaId = v.ProvinciaId,
                CategoriaId = v.CategoriaId,
                Titulo = v.Titulo,
                TipoContrato = v.TipoContrato,
                SalarioCompensacion = v.SalarioCompensacion,
                FechaLimiteAplicacion = v.FechaLimiteAplicacion,
                HorarioTrabajo = v.HorarioTrabajo,
                DuracionContrato = v.DuracionContrato,
                DescripcionPuesto = v.DescripcionPuesto,
                ResponsabilidadesEspecificas = v.ResponsabilidadesEspecificas,
                RequisitosGenerales = v.RequisitosGenerales,
                EducacionRequerida = v.EducacionRequerida,
                ExperienciaRequerida = v.ExperienciaRequerida,
                HabilidadesCompetencias = v.HabilidadesCompetencias,
                BeneficiosCompensaciones = v.BeneficiosCompensaciones,
                InformacionContacto = v.InformacionContacto,
                IsActive = v.IsActive,
                CreatedAt = v.CreatedAt,
                UpdatedAt = v.UpdatedAt,
                Institucion = new Institucion
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    CodigoNombre = i.CodigoNombre,
                },
                Provincia = new Provincia { Id = p.Id, Nombre = p.Nombre },
                Categoria = new CategoriaVacante { Id = c.Id, Nombre = c.Nombre },
            }
        ).FirstOrDefaultAsync();
    }

    public async Task<Vacante> CreateAsync(Vacante vacante)
    {
        _context.Vacantes.Add(vacante);
        await _context.SaveChangesAsync();
        return vacante;
    }

    public async Task<Vacante> UpdateAsync(Vacante vacante)
    {
        _context.Vacantes.Update(vacante);
        await _context.SaveChangesAsync();
        return vacante;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var vacante = await GetByIdAsync(id);
        if (vacante == null)
            return false;

        _context.Vacantes.Remove(vacante);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<Vacante>> GetByInstitucionWithDetailsAsync(int institucionId)
    {
        return await (
            from v in _context.Vacantes
            join i in _context.Instituciones on v.InstitucionId equals i.Id
            join p in _context.Provincias on v.ProvinciaId equals p.Id
            join c in _context.CategoriasVacante on v.CategoriaId equals c.Id
            where v.InstitucionId == institucionId
            orderby v.CreatedAt descending
            select new Vacante
            {
                Id = v.Id,
                InstitucionId = v.InstitucionId,
                ProvinciaId = v.ProvinciaId,
                CategoriaId = v.CategoriaId,
                Titulo = v.Titulo,
                TipoContrato = v.TipoContrato,
                SalarioCompensacion = v.SalarioCompensacion,
                FechaLimiteAplicacion = v.FechaLimiteAplicacion,
                HorarioTrabajo = v.HorarioTrabajo,
                DuracionContrato = v.DuracionContrato,
                DescripcionPuesto = v.DescripcionPuesto,
                ResponsabilidadesEspecificas = v.ResponsabilidadesEspecificas,
                RequisitosGenerales = v.RequisitosGenerales,
                EducacionRequerida = v.EducacionRequerida,
                ExperienciaRequerida = v.ExperienciaRequerida,
                HabilidadesCompetencias = v.HabilidadesCompetencias,
                BeneficiosCompensaciones = v.BeneficiosCompensaciones,
                InformacionContacto = v.InformacionContacto,
                IsActive = v.IsActive,
                CreatedAt = v.CreatedAt,
                UpdatedAt = v.UpdatedAt,
                Institucion = new Institucion
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    CodigoNombre = i.CodigoNombre,
                },
                Provincia = new Provincia { Id = p.Id, Nombre = p.Nombre },
                Categoria = new CategoriaVacante { Id = c.Id, Nombre = c.Nombre },
            }
        ).ToListAsync();
    }

    public async Task<List<Vacante>> GetByCategoriaWithDetailsAsync(int categoriaId)
    {
        return await (
            from v in _context.Vacantes
            join i in _context.Instituciones on v.InstitucionId equals i.Id
            join p in _context.Provincias on v.ProvinciaId equals p.Id
            join c in _context.CategoriasVacante on v.CategoriaId equals c.Id
            where v.CategoriaId == categoriaId
            orderby v.CreatedAt descending
            select new Vacante
            {
                Id = v.Id,
                InstitucionId = v.InstitucionId,
                ProvinciaId = v.ProvinciaId,
                CategoriaId = v.CategoriaId,
                Titulo = v.Titulo,
                TipoContrato = v.TipoContrato,
                SalarioCompensacion = v.SalarioCompensacion,
                FechaLimiteAplicacion = v.FechaLimiteAplicacion,
                HorarioTrabajo = v.HorarioTrabajo,
                DuracionContrato = v.DuracionContrato,
                DescripcionPuesto = v.DescripcionPuesto,
                ResponsabilidadesEspecificas = v.ResponsabilidadesEspecificas,
                RequisitosGenerales = v.RequisitosGenerales,
                EducacionRequerida = v.EducacionRequerida,
                ExperienciaRequerida = v.ExperienciaRequerida,
                HabilidadesCompetencias = v.HabilidadesCompetencias,
                BeneficiosCompensaciones = v.BeneficiosCompensaciones,
                InformacionContacto = v.InformacionContacto,
                IsActive = v.IsActive,
                CreatedAt = v.CreatedAt,
                UpdatedAt = v.UpdatedAt,
                Institucion = new Institucion
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    CodigoNombre = i.CodigoNombre,
                },
                Provincia = new Provincia { Id = p.Id, Nombre = p.Nombre },
                Categoria = new CategoriaVacante { Id = c.Id, Nombre = c.Nombre },
            }
        ).ToListAsync();
    }

    public async Task<List<Vacante>> GetByProvinciaWithDetailsAsync(int provinciaId)
    {
        return await (
            from v in _context.Vacantes
            join i in _context.Instituciones on v.InstitucionId equals i.Id
            join p in _context.Provincias on v.ProvinciaId equals p.Id
            join c in _context.CategoriasVacante on v.CategoriaId equals c.Id
            where v.ProvinciaId == provinciaId
            orderby v.CreatedAt descending
            select new Vacante
            {
                Id = v.Id,
                InstitucionId = v.InstitucionId,
                ProvinciaId = v.ProvinciaId,
                CategoriaId = v.CategoriaId,
                Titulo = v.Titulo,
                TipoContrato = v.TipoContrato,
                SalarioCompensacion = v.SalarioCompensacion,
                FechaLimiteAplicacion = v.FechaLimiteAplicacion,
                HorarioTrabajo = v.HorarioTrabajo,
                DuracionContrato = v.DuracionContrato,
                DescripcionPuesto = v.DescripcionPuesto,
                ResponsabilidadesEspecificas = v.ResponsabilidadesEspecificas,
                RequisitosGenerales = v.RequisitosGenerales,
                EducacionRequerida = v.EducacionRequerida,
                ExperienciaRequerida = v.ExperienciaRequerida,
                HabilidadesCompetencias = v.HabilidadesCompetencias,
                BeneficiosCompensaciones = v.BeneficiosCompensaciones,
                InformacionContacto = v.InformacionContacto,
                IsActive = v.IsActive,
                CreatedAt = v.CreatedAt,
                UpdatedAt = v.UpdatedAt,
                Institucion = new Institucion
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    CodigoNombre = i.CodigoNombre,
                },
                Provincia = new Provincia { Id = p.Id, Nombre = p.Nombre },
                Categoria = new CategoriaVacante { Id = c.Id, Nombre = c.Nombre },
            }
        ).ToListAsync();
    }

    public async Task<bool> SetActiveAsync(int id, bool active)
    {
        var v = await _context.Vacantes.FirstOrDefaultAsync(x => x.Id == id);
        if (v is null)
            return false;

        v.IsActive = active;
        v.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> InstitucionExistsAsync(int institucionId)
    {
        return await _context.Instituciones.AnyAsync(i => i.Id == institucionId);
    }

    public async Task<bool> ProvinciaExistsAsync(int provinciaId)
    {
        return await _context.Provincias.AnyAsync(p => p.Id == provinciaId);
    }

    public async Task<bool> CategoriaExistsAsync(int categoriaId)
    {
        return await _context.CategoriasVacante.AnyAsync(c => c.Id == categoriaId);
    }
}
