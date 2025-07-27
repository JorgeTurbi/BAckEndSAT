using AutoMapper;
using Context;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Services.Instituciones;

namespace Repository.InstitucionesRepository;

public class InstitucionRepository : IInstitucion
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _Mapper;
    public InstitucionRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _Mapper = mapper;
    }

    public async Task<GenericResponseDto<List<InstitucionDto>>> GetAllAsync()
    {
       var instituciones = await _context.Instituciones.ToListAsync();

       if (instituciones == null || !instituciones.Any())
        {
            return new GenericResponseDto<List<InstitucionDto>>
            {
                Success = false,
                Message = "No se encontraron instituciones",
                Data = null
            };
        }
        var institucionesDto = _Mapper.Map<List<InstitucionDto>>(instituciones);

        return new GenericResponseDto<List<InstitucionDto>>
        {
            Success = true,
            Message = "Instituciones obtenidas exitosamente",
            Data = institucionesDto
        };
    }

    public async Task<GenericResponseDto<List<DepartamentoDto>>> GetAllDepartamentosAsync()
    {
     

var departamentos = await _context.Departamentos.ToListAsync();

       if (departamentos == null || !departamentos.Any())
        {
            return new GenericResponseDto<List<DepartamentoDto>>
            {
                Success = false,
                Message = "No se encontraron instituciones",
                Data = null
            };
        }
        var DeptoDto = _Mapper.Map<List<DepartamentoDto>>(departamentos);

        return new GenericResponseDto<List<DepartamentoDto>>
        {
            Success = true,
            Message = "Instituciones obtenidas exitosamente",
            Data = DeptoDto
        };
    }
}

