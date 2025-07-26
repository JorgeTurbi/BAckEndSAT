using DTOs;

namespace Services;

public interface ICategoriaVacanteService
{
    Task<GenericResponseDto<List<CategoriaVacanteDto>>> GetAllAsync();
    Task<GenericResponseDto<CategoriaVacanteDto>> GetByIdAsync(int id);
    Task<GenericResponseDto<CategoriaVacanteDto>> CreateAsync(CategoriaVacanteDto categoriaDto);
    Task<GenericResponseDto<CategoriaVacanteDto>> UpdateAsync(CategoriaVacanteDto categoriaDto);
    Task<GenericResponseDto<bool>> DeleteAsync(int id);
}
