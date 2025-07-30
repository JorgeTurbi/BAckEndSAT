using AutoMapper;
using DTOs;
using Entities;
using Repository;

namespace Services;

public class VacanteService : IVacanteService
{
    private readonly IVacanteRepository _vacanteRepository;
    private readonly IMapper _mapper;

    public VacanteService(IVacanteRepository vacanteRepository, IMapper mapper)
    {
        _vacanteRepository = vacanteRepository;
        _mapper = mapper;
    }

    public async Task<GenericResponseDto<List<VacanteDto>>> GetAllAsync()
    {
        try
        {
            var vacantes = await _vacanteRepository.GetAllWithDetailsAsync();
            var vacantesDto = _mapper.Map<List<VacanteDto>>(vacantes);

            return new GenericResponseDto<List<VacanteDto>>
            {
                Success = true,
                Message = "Vacantes obtenidas exitosamente",
                Data = vacantesDto,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<List<VacanteDto>>
            {
                Success = false,
                Message = $"Error al obtener vacantes: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<GenericResponseDto<List<VacanteDto>>> GetActiveAsync()
    {
        try
        {
            var vacantes = await _vacanteRepository.GetActiveWithDetailsAsync();
            var vacantesDto = _mapper.Map<List<VacanteDto>>(vacantes);

            return new GenericResponseDto<List<VacanteDto>>
            {
                Success = true,
                Message = "Vacantes activas obtenidas exitosamente",
                Data = vacantesDto,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<List<VacanteDto>>
            {
                Success = false,
                Message = $"Error al obtener vacantes activas: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<GenericResponseDto<VacanteDto>> GetByIdAsync(int id)
    {
        try
        {
            var vacante = await _vacanteRepository.GetByIdWithDetailsAsync(id);
            if (vacante == null)
            {
                return new GenericResponseDto<VacanteDto>
                {
                    Success = false,
                    Message = "Vacante no encontrada",
                    Data = null,
                };
            }

            var vacanteDto = _mapper.Map<VacanteDto>(vacante);

            return new GenericResponseDto<VacanteDto>
            {
                Success = true,
                Message = "Vacante obtenida exitosamente",
                Data = vacanteDto,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<VacanteDto>
            {
                Success = false,
                Message = $"Error al obtener vacante: {ex.Message}",
                Data = null,
            };
        }
    }

    // public async Task<GenericResponseDto<VacanteDto>> CreateAsync(VacanteCreateDto vacanteDto)
    // {
    //     try
    //     {
    //         // Validar que existan las entidades relacionadas
    //         var validationResult = await ValidateRelatedEntitiesAsync(
    //             vacanteDto.InstitucionId,
    //             vacanteDto.ProvinciaId,
    //             vacanteDto.CategoriaId
    //         );
    //         if (!validationResult.Success)
    //         {
    //             return new GenericResponseDto<VacanteDto>
    //             {
    //                 Success = false,
    //                 Message = validationResult.Message,
    //                 Data = null,
    //             };
    //         }

    //         var vacante = _mapper.Map<Vacante>(vacanteDto);
    //         vacante.CreatedAt = DateTime.UtcNow;

    //         var createdVacante = await _vacanteRepository.CreateAsync(vacanteDto);
    //         var vacanteWithDetails = await _vacanteRepository.GetByIdWithDetailsAsync(
    //             createdVacante.Id
    //         );
    //         var createdVacanteDto = _mapper.Map<VacanteDto>(vacanteWithDetails);

    //         return new GenericResponseDto<VacanteDto>
    //         {
    //             Success = true,
    //             Message = "Vacante creada exitosamente",
    //             Data = createdVacanteDto,
    //         };
    //     }
    //     catch (Exception ex)
    //     {
    //         return new GenericResponseDto<VacanteDto>
    //         {
    //             Success = false,
    //             Message = $"Error al crear vacante: {ex.Message}",
    //             Data = null,
    //         };
    //     }
    // }

    public async Task<GenericResponseDto<VacanteDto>> UpdateAsync(VacanteUpdateDto vacanteDto)
    {
        try
        {
            var existingVacante = await _vacanteRepository.GetByIdAsync(vacanteDto.Id);
            if (existingVacante == null)
            {
                return new GenericResponseDto<VacanteDto>
                {
                    Success = false,
                    Message = "Vacante no encontrada",
                    Data = null,
                };
            }

            // Validar que existan las entidades relacionadas
            var validationResult = await ValidateRelatedEntitiesAsync(
                vacanteDto.InstitucionId,
                vacanteDto.ProvinciaId,
                vacanteDto.CategoriaId
            );
            if (!validationResult.Success)
            {
                return new GenericResponseDto<VacanteDto>
                {
                    Success = false,
                    Message = validationResult.Message,
                    Data = null,
                };
            }

            _mapper.Map(vacanteDto, existingVacante);
            existingVacante.UpdatedAt = DateTime.UtcNow;

            var updatedVacante = await _vacanteRepository.UpdateAsync(existingVacante);
            var vacanteWithDetails = await _vacanteRepository.GetByIdWithDetailsAsync(
                updatedVacante.Id
            );
            var updatedVacanteDto = _mapper.Map<VacanteDto>(vacanteWithDetails);

            return new GenericResponseDto<VacanteDto>
            {
                Success = true,
                Message = "Vacante actualizada exitosamente",
                Data = updatedVacanteDto,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<VacanteDto>
            {
                Success = false,
                Message = $"Error al actualizar vacante: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<GenericResponseDto<bool>> DeleteAsync(int id)
    {
        try
        {
            var vacante = await _vacanteRepository.GetByIdAsync(id);
            if (vacante == null)
            {
                return new GenericResponseDto<bool>
                {
                    Success = false,
                    Message = "Vacante no encontrada",
                    Data = false,
                };
            }

            var deleted = await _vacanteRepository.DeleteAsync(id);

            return new GenericResponseDto<bool>
            {
                Success = deleted,
                Message = deleted
                    ? "Vacante eliminada exitosamente"
                    : "No se pudo eliminar la vacante",
                Data = deleted,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Message = $"Error al eliminar vacante: {ex.Message}",
                Data = false,
            };
        }
    }

    public async Task<GenericResponseDto<List<VacanteDto>>> GetByInstitucionAsync(int institucionId)
    {
        try
        {
            var vacantes = await _vacanteRepository.GetByInstitucionWithDetailsAsync(institucionId);
            var vacantesDto = _mapper.Map<List<VacanteDto>>(vacantes);

            return new GenericResponseDto<List<VacanteDto>>
            {
                Success = true,
                Message = "Vacantes de la institución obtenidas exitosamente",
                Data = vacantesDto,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<List<VacanteDto>>
            {
                Success = false,
                Message = $"Error al obtener vacantes de la institución: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<GenericResponseDto<List<VacanteDto>>> GetByCategoriaAsync(int categoriaId)
    {
        try
        {
            var vacantes = await _vacanteRepository.GetByCategoriaWithDetailsAsync(categoriaId);
            var vacantesDto = _mapper.Map<List<VacanteDto>>(vacantes);

            return new GenericResponseDto<List<VacanteDto>>
            {
                Success = true,
                Message = "Vacantes de la categoría obtenidas exitosamente",
                Data = vacantesDto,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<List<VacanteDto>>
            {
                Success = false,
                Message = $"Error al obtener vacantes de la categoría: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<GenericResponseDto<List<VacanteDto>>> GetByProvinciaAsync(int provinciaId)
    {
        try
        {
            var vacantes = await _vacanteRepository.GetByProvinciaWithDetailsAsync(provinciaId);
            var vacantesDto = _mapper.Map<List<VacanteDto>>(vacantes);

            return new GenericResponseDto<List<VacanteDto>>
            {
                Success = true,
                Message = "Vacantes de la provincia obtenidas exitosamente",
                Data = vacantesDto,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<List<VacanteDto>>
            {
                Success = false,
                Message = $"Error al obtener vacantes de la provincia: {ex.Message}",
                Data = null,
            };
        }
    }

    private async Task<GenericResponseDto<bool>> ValidateRelatedEntitiesAsync(
        int institucionId,
        int provinciaId,
        int categoriaId
    )
    {
        var institucionExists = await _vacanteRepository.InstitucionExistsAsync(institucionId);
        if (!institucionExists)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Message = "La institución especificada no existe",
            };
        }

        var provinciaExists = await _vacanteRepository.ProvinciaExistsAsync(provinciaId);
        if (!provinciaExists)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Message = "La provincia especificada no existe",
            };
        }

        var categoriaExists = await _vacanteRepository.CategoriaExistsAsync(categoriaId);
        if (!categoriaExists)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Message = "La categoría especificada no existe",
            };
        }

        return new GenericResponseDto<bool> { Success = true };
    }

    public async Task<GenericResponseDto<bool>> ActivateAsync(int id)
    {
        var v = await _vacanteRepository.GetByIdAsync(id);
        if (v is null)
            return new()
            {
                Success = false,
                Message = "Vacante no encontrada",
                Data = false,
            };

        if (v.IsActive)
            return new()
            {
                Success = true,
                Message = "La vacante ya estaba activa",
                Data = true,
            };

        var ok = await _vacanteRepository.SetActiveAsync(id, true);

        // Nota: si la fecha límite venció, seguirá activa internamente
        // pero GetActive() no la listará por el filtro de fecha.
        return new()
        {
            Success = ok,
            Message = ok ? "Vacante activada" : "No se pudo activar la vacante",
            Data = ok,
        };
    }

    public async Task<GenericResponseDto<bool>> DeactivateAsync(int id)
    {
        var v = await _vacanteRepository.GetByIdAsync(id);
        if (v is null)
            return new()
            {
                Success = false,
                Message = "Vacante no encontrada",
                Data = false,
            };

        if (!v.IsActive)
            return new()
            {
                Success = true,
                Message = "La vacante ya estaba desactivada",
                Data = true,
            };

        var ok = await _vacanteRepository.SetActiveAsync(id, false);

        return new()
        {
            Success = ok,
            Message = ok ? "Vacante desactivada" : "No se pudo desactivar la vacante",
            Data = ok,
        };
    }
}
