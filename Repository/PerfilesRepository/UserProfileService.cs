using AutoMapper;
using Context;
using DTOs;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Perfil;

namespace Repository.PerlesRepository;

public class UserProfileService : IUserProfileService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UserProfileService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GenericResponseDto<bool>> CreateAsync(ApplicanteDto dto)
    {
        var entities = await _context.Aplicantes.Where(a => a.Cedula == dto.Cedula)
                                         .Include(u => u.Experience)
                                         .Include(u => u.Education)
                                         .FirstOrDefaultAsync();
        if (entities!=null)
        {
            return new GenericResponseDto<bool> { Success = false, Message = "Perfil Existe", Data = false! };
        }
        if (entities!.Email == dto.Email)
        {
            return new GenericResponseDto<bool> { Success = false, Message = "Correo ya Existe", Data = false! };
        }
        var mapped = _mapper.Map<Aplicante>(entities);

        await _context.Aplicantes.AddAsync(mapped);
        var result = await _context.SaveChangesAsync() > 0 ? true : false;
        if (result)
        {
            return new GenericResponseDto<bool> { Success = result, Message = "Perfil creado!", Data = result };
        }
        else
        {
            return new GenericResponseDto<bool> { Success = result, Message = "Hubo un problema al crear el perfil", Data = result };
        }
      



    }   

    public async Task<GenericResponseDto<ApplicanteDto>> GetByIdAsync(int id)
    {
        var result = await _context.Aplicantes.FindAsync(id);
        if (result != null)
        {
            var mapped = _mapper.Map<ApplicanteDto>(result);
            return new GenericResponseDto<ApplicanteDto>
            {
                Success = true,
                Message = "Perfil encontrado",
                Data = mapped
            };
        }
          return new GenericResponseDto<ApplicanteDto>
            {
                Success = false,
                Message = "No existe datos",
                Data= null!
             };
    }

  

}