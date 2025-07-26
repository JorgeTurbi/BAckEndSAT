using DTOs;

namespace Services;

public interface IProvinciaService
{
    Task<GenericResponseDto<List<ProvinciaDto>>> GetAllAsync();
    Task<GenericResponseDto<ProvinciaDto>> GetByIdAsync(int id);
}
