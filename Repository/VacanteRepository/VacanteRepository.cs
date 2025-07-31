using System.Linq;
using AutoMapper;
using Context;
using DTOs;
using Entities;
using Helper;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class VacanteRepository : IVacanteRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public VacanteRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
public async Task<GenericResponseDto<List<VacanteDto>>> GetAllWithDetailsAsync()
{
    // 1️⃣ Ejecuta la consulta en la DB de forma asíncrona
    var rawData = await (
        from v in _context.Vacantes
        join i in _context.Instituciones on v.InstitucionId equals i.Id
        join p in _context.Provincias on v.ProvinciaId equals p.Id
        join c in _context.CategoriasVacante on v.CategoriaId equals c.Id
        join u in _context.Users on v.UserId equals u.Id
        where v.IsActive == true
        orderby v.CreatedAt descending
        select new { v, i, p, c, u }
    ).ToListAsync(); // 👈 Aquí ya usamos await

    // 2️⃣ Transformamos a DTO en memoria
    var queryData = rawData.Select(x => new VacanteDto
    {
        Id = x.v.Id,
        InstitucionId = x.v.InstitucionId,
        ProvinciaId = x.v.ProvinciaId,
        ProvinciaNombre = x.p.Nombre,
        CategoriaId = x.v.CategoriaId,
        Titulo = x.v.Titulo,
        TipoContrato = x.v.TipoContrato,
        SalarioCompensacion = x.v.SalarioCompensacion,
        FechaLimiteAplicacion = x.v.FechaLimiteAplicacion,
        HorarioTrabajo = x.v.HorarioTrabajo,
        DuracionContrato = x.v.DuracionContrato,
        DescripcionPuesto = x.v.DescripcionPuesto,
        ResponsabilidadesEspecificas = x.v.ResponsabilidadesEspecificas?.Split(","),
        RequisitosGenerales = x.v.RequisitosGenerales.Split(","), // ahora sí funciona
        EducacionRequerida = x.v.EducacionRequerida,
        ExperienciaRequerida = x.v.ExperienciaRequerida,
        HabilidadesCompetencias = x.v.HabilidadesCompetencias?.Split(","),
        BeneficiosCompensaciones = x.v.BeneficiosCompensaciones?.Split(","),
        Telefono = x.v.Telefono,
        Email=x.v.Email,
        IsActive = x.v.IsActive,
        CreatedAt = x.v.CreatedAt,
        UpdatedAt = x.v.UpdatedAt,
        InstitucionNombre = x.i.Nombre,
        CategoriaNombre = x.c.Nombre,
        UserId = x.u.Id
    }).ToList();

    return new GenericResponseDto<List<VacanteDto>>
    {
        Success = queryData.Count > 0,
        Message = queryData.Count > 0 
            ? $"Se encontraron {queryData.Count} vacantes activas" 
            : "No existen datos para mostrar",
        Data = queryData.Count > 0 ? queryData : null
    };
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
                Telefono = v.Telefono,
                Email=v.Email,
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

public Task<GenericResponseDto<VacanteDto>> GetByIdWithDetailsAsync(int id)
{
    var queryData = (
        from v in _context.Vacantes
        join i in _context.Instituciones on v.InstitucionId equals i.Id
        join p in _context.Provincias on v.ProvinciaId equals p.Id
        join c in _context.CategoriasVacante on v.CategoriaId equals c.Id
        join u in _context.Users on v.UserId equals u.Id
        where v.IsActive && v.Id == id
        orderby v.CreatedAt descending
        select new
        {
            v,
            InstitucionNombre = i.Nombre,
            ProvinciaNombre = p.Nombre,
            CategoriaNombre = c.Nombre,
            UserId = u.Id
        }
    ).AsEnumerable() // ejecuta en memoria
    .Select(x => new VacanteDto
    {
        Id = x.v.Id,
        InstitucionId = x.v.InstitucionId,
        ProvinciaId = x.v.ProvinciaId,
        ProvinciaNombre = x.ProvinciaNombre,
        CategoriaId = x.v.CategoriaId,
        Titulo = x.v.Titulo,
        TipoContrato = x.v.TipoContrato,
        SalarioCompensacion = x.v.SalarioCompensacion,
        FechaLimiteAplicacion = x.v.FechaLimiteAplicacion,
        HorarioTrabajo = x.v.HorarioTrabajo,
        DuracionContrato = x.v.DuracionContrato,
        DescripcionPuesto = x.v.DescripcionPuesto,
        ResponsabilidadesEspecificas = string.IsNullOrEmpty(x.v.ResponsabilidadesEspecificas)
            ? Array.Empty<string>()
            : x.v.ResponsabilidadesEspecificas.Split(','),
        RequisitosGenerales = string.IsNullOrEmpty(x.v.RequisitosGenerales)
            ? Array.Empty<string>()
            : x.v.RequisitosGenerales.Split(','),
        EducacionRequerida = x.v.EducacionRequerida,
        ExperienciaRequerida = x.v.ExperienciaRequerida,
        HabilidadesCompetencias = string.IsNullOrEmpty(x.v.HabilidadesCompetencias)
            ? Array.Empty<string>()
            : x.v.HabilidadesCompetencias.Split(','),
        BeneficiosCompensaciones = string.IsNullOrEmpty(x.v.BeneficiosCompensaciones)
            ? Array.Empty<string>()
            : x.v.BeneficiosCompensaciones.Split(','),
        Telefono = x.v.Telefono,
        Email = x.v.Email,
        IsActive = x.v.IsActive,
        CreatedAt = x.v.CreatedAt,
        UpdatedAt = x.v.UpdatedAt,
        InstitucionNombre = x.InstitucionNombre,
        CategoriaNombre = x.CategoriaNombre,
        UserId = x.UserId
    })
    .FirstOrDefault();

    var response = new GenericResponseDto<VacanteDto>
    {
        Success = queryData != null,
        Message = queryData != null ? "Vacante encontrada" : "No se encontró la vacante",
        Data = queryData!
    };

    // 🔹 Envuelve el resultado en un Task para cumplir con la firma
    return Task.FromResult(response);
}


    public async Task<GenericResponseDto<bool>> CreateAsync(VacanteCreateDto dto)
    {

        var validation = ValidatorHelper.Validate(dto);
        if (!validation.Success)
        {

            return new GenericResponseDto<bool>
            {
                Success = false,
                Message = validation.Message,
                Data = false
            };
        }

        var mapped = _mapper.Map<Vacante>(dto);
        mapped.CreatedAt = DateTime.UtcNow;


        await _context.Vacantes.AddAsync(mapped);
        bool result = await _context.SaveChangesAsync() > 0 ? true : false;
        return new GenericResponseDto<bool>
        {
            Success = result,
            Message = result == true ? "Nueva vacante publicada" : "No se pudo registrar la vacante",
            Data = result
        };

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
                Telefono = v.Telefono,
                Email=v.Email,
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
                Telefono = v.Telefono,
                Email=v.Email,
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
                Telefono = v.Telefono,
                Email=v.Email,
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
