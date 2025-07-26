using AutoMapper;
using DTOs;
using Entities;
using Repository;

namespace Services;

public class CategoriaVacanteService : ICategoriaVacanteService
{
    private readonly ICategoriaVacanteRepository _categoriaRepository;
    private readonly IMapper _mapper;

    public CategoriaVacanteService(ICategoriaVacanteRepository categoriaRepository, IMapper mapper)
    {
        _categoriaRepository = categoriaRepository;
        _mapper = mapper;
    }

    public async Task<GenericResponseDto<List<CategoriaVacanteDto>>> GetAllAsync()
    {
        try
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            var categoriasDto = _mapper.Map<List<CategoriaVacanteDto>>(categorias);

            return new GenericResponseDto<List<CategoriaVacanteDto>>
            {
                Success = true,
                Message = "Categorías obtenidas exitosamente",
                Data = categoriasDto,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<List<CategoriaVacanteDto>>
            {
                Success = false,
                Message = $"Error al obtener categorías: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<GenericResponseDto<CategoriaVacanteDto>> GetByIdAsync(int id)
    {
        try
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria == null)
            {
                return new GenericResponseDto<CategoriaVacanteDto>
                {
                    Success = false,
                    Message = "Categoría no encontrada",
                    Data = null,
                };
            }

            var categoriaDto = _mapper.Map<CategoriaVacanteDto>(categoria);

            return new GenericResponseDto<CategoriaVacanteDto>
            {
                Success = true,
                Message = "Categoría obtenida exitosamente",
                Data = categoriaDto,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<CategoriaVacanteDto>
            {
                Success = false,
                Message = $"Error al obtener categoría: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<GenericResponseDto<CategoriaVacanteDto>> CreateAsync(
        CategoriaVacanteDto categoriaDto
    )
    {
        try
        {
            // Validar que no exista otra categoría con el mismo nombre
            var existingCategoria = await _categoriaRepository.GetByNombreAsync(
                categoriaDto.Nombre
            );
            if (existingCategoria != null)
            {
                return new GenericResponseDto<CategoriaVacanteDto>
                {
                    Success = false,
                    Message = "Ya existe una categoría con ese nombre",
                    Data = null,
                };
            }

            var categoria = _mapper.Map<CategoriaVacante>(categoriaDto);
            var createdCategoria = await _categoriaRepository.CreateAsync(categoria);
            var createdCategoriaDto = _mapper.Map<CategoriaVacanteDto>(createdCategoria);

            return new GenericResponseDto<CategoriaVacanteDto>
            {
                Success = true,
                Message = "Categoría creada exitosamente",
                Data = createdCategoriaDto,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<CategoriaVacanteDto>
            {
                Success = false,
                Message = $"Error al crear categoría: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<GenericResponseDto<CategoriaVacanteDto>> UpdateAsync(
        CategoriaVacanteDto categoriaDto
    )
    {
        try
        {
            var existingCategoria = await _categoriaRepository.GetByIdAsync(categoriaDto.Id);
            if (existingCategoria == null)
            {
                return new GenericResponseDto<CategoriaVacanteDto>
                {
                    Success = false,
                    Message = "Categoría no encontrada",
                    Data = null,
                };
            }

            // Validar que no exista otra categoría con el mismo nombre (excluyendo la actual)
            var duplicateCategoria = await _categoriaRepository.GetByNombreAsync(
                categoriaDto.Nombre
            );
            if (duplicateCategoria != null && duplicateCategoria.Id != categoriaDto.Id)
            {
                return new GenericResponseDto<CategoriaVacanteDto>
                {
                    Success = false,
                    Message = "Ya existe una categoría con ese nombre",
                    Data = null,
                };
            }

            _mapper.Map(categoriaDto, existingCategoria);
            var updatedCategoria = await _categoriaRepository.UpdateAsync(existingCategoria);
            var updatedCategoriaDto = _mapper.Map<CategoriaVacanteDto>(updatedCategoria);

            return new GenericResponseDto<CategoriaVacanteDto>
            {
                Success = true,
                Message = "Categoría actualizada exitosamente",
                Data = updatedCategoriaDto,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<CategoriaVacanteDto>
            {
                Success = false,
                Message = $"Error al actualizar categoría: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<GenericResponseDto<bool>> DeleteAsync(int id)
    {
        try
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria == null)
            {
                return new GenericResponseDto<bool>
                {
                    Success = false,
                    Message = "Categoría no encontrada",
                    Data = false,
                };
            }

            // Verificar si tiene vacantes asociadas
            var hasVacantes = await _categoriaRepository.HasVacantesAsync(id);
            if (hasVacantes)
            {
                return new GenericResponseDto<bool>
                {
                    Success = false,
                    Message = "No se puede eliminar la categoría porque tiene vacantes asociadas",
                    Data = false,
                };
            }

            var deleted = await _categoriaRepository.DeleteAsync(id);

            return new GenericResponseDto<bool>
            {
                Success = deleted,
                Message = deleted
                    ? "Categoría eliminada exitosamente"
                    : "No se pudo eliminar la categoría",
                Data = deleted,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Message = $"Error al eliminar categoría: {ex.Message}",
                Data = false,
            };
        }
    }
}
