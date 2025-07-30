using AutoMapper;
using Context;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Services.Perfil;

namespace Repository.InstitucionesRepository.PerfilesRepository;

public class Generico : IInterfacePerfil
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public Generico(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GenericResponseDto<List<InstitucionMilitarDTO>>> GetInstituciones()
    {
        var query = await _context.InstitucionMilitar.ToListAsync();
        if (query.Count > 0)
        {
            var Selected = _mapper.Map<List<InstitucionMilitarDTO>>(query);

            return new GenericResponseDto<List<InstitucionMilitarDTO>>
            {
                Success = true,
                Message = $"Se encontraron {Selected.Count}",
                Data = Selected
            };
        }
        
        return new GenericResponseDto<List<InstitucionMilitarDTO>>
            {
                Success = true,
                Message = $"Se encontraron {query.Count}",
                Data = null!
            };
    }

    public async Task<GenericResponseDto<List<RangoDTO>>> GetRangos()
    {
        var query = await _context.Rangos.ToListAsync();

        if (query.Count > 0)
        {
            var Selected = _mapper.Map<List<RangoDTO>>(query);

            return new GenericResponseDto<List<RangoDTO>>
            {
                Success = true,
                Message = $"Se encontraron {Selected.Count}",
                Data = Selected
            };
        }
        
         return new GenericResponseDto<List<RangoDTO>>
        {
            Success = false,
            Message = $"Se encontraron {query.Count}",
            Data = null!
        };
       
    }
}