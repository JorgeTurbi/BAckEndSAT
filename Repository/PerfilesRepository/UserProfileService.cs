using System.Runtime.CompilerServices;
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
        // 1. Verificar si ya existe la cédula
        var existingApplicant = await _context.Aplicantes
            .Where(a => a.Cedula == dto.Cedula || a.Email == dto.Email || a.UserId== dto.UserId)
            .Include(u => u.Experience)
            .Include(u => u.Education)
            .FirstOrDefaultAsync();

        if (existingApplicant != null)
        {
            // Verificar si es por correo o por cédula
            var message = existingApplicant.Cedula == dto.Cedula
                ? "Perfil  ya existe"
                : "Registro ya existe";

            return new GenericResponseDto<bool>
            {
                Success = false,
                Message = message,
                Data = false
            };
        }

        // 2. Mapear desde el DTO, no desde la entidad existente
        var aplicante = _mapper.Map<Aplicante>(dto);
        aplicante.CreateAt = DateTime.UtcNow;
        var aplicanteEducation = _mapper.Map<List<Education>>(dto.Education);
        var aplicanteExperience = _mapper.Map<List<Experience>>(dto.Experience);

         // 3. Usar transacción para asegurar consistencia
    using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // Guardar aplicante
            await _context.Aplicantes.AddAsync(aplicante);
            var result1 = await _context.SaveChangesAsync() > 0;

            if (!result1)
            {
                return new GenericResponseDto<bool>
                {
                    Success = false,
                    Message = "Error al crear aplicante",
                    Data = false
                };
            }
        await transaction.CommitAsync();
      return new GenericResponseDto<bool> 
                { 
                    Success = true, 
                    Message = "Perfil creado!", 
                    Data = true 
                };

        // var aplicanteId = aplicante.Id;

            // // Guardar Education
            // foreach (var item in aplicanteEducation)
            // {
            //     item.AplicanteId = aplicanteId;
            // }
            // await _context.Educations.AddRangeAsync(aplicanteEducation);
            // var result2 = await _context.SaveChangesAsync() > 0;

            // // Guardar Experience
            // foreach (var item in aplicanteExperience)
            // {
            //     item.AplicanteId = aplicanteId;
            // }
            // await _context.Experiences.AddRangeAsync(aplicanteExperience);
            // var result3 = await _context.SaveChangesAsync() > 0;

            // // Confirmar transacción
            // await transaction.CommitAsync();

            // if (result1 && result2 && result3)
            // {
            //     return new GenericResponseDto<bool> 
            //     { 
            //         Success = true, 
            //         Message = "Perfil creado!", 
            //         Data = true 
            //     };
            // }

            // return new GenericResponseDto<bool> 
            // { 
            //     Success = false, 
            //     Message = "Hubo un problema al crear el perfil", 
            //     Data = false 
            // };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new GenericResponseDto<bool>
            {
                Success = false,
                Message = ex.Message,
                Data = false
            };
        }

    }

    public async Task<GenericResponseDto<ApplicanteDto>> GetByIdAsync(int UserId)
    {
         // 1. Consultar aplicante con sus relaciones
    var result = await _context.Aplicantes.Where(a => a.UserId == UserId)
        .Include(a => a.Education)
        .Include(a => a.Experience)             
        .FirstOrDefaultAsync();
      
    if (result == null)
        {
            return new GenericResponseDto<ApplicanteDto>
            {
                Success = false,
                Message = "No existe datos",
                Data = null!
            };
        }
        int idRango = Convert.ToInt16(result.Rank);
        string? rango = string.Empty;
   

if (idRango>0)
{
    rango = await _context.Rangos
        .Where(a => a.Id == idRango)
        .Select(a => a.Nombre)
        .FirstOrDefaultAsync();
}

        // 2. Mapear al DTO (AutoMapper manejará listas si están configuradas)
    
        var mapped = _mapper.Map<ApplicanteDto>(result);
        mapped.Rank = rango;

    return new GenericResponseDto<ApplicanteDto>
        {
            Success = true,
            Message = "Perfil encontrado",
            Data = mapped
        };
    }



}