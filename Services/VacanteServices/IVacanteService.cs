using DTOs;

namespace Services;

public interface IVacanteService
{
    Task<GenericResponseDto<List<VacanteDto>>> GetAllAsync();
    Task<GenericResponseDto<List<VacanteDto>>> GetActiveAsync();
    Task<GenericResponseDto<VacanteDto>> GetByIdAsync(int id);
    // Task<GenericResponseDto<VacanteDto>> CreateAsync(VacanteCreateDto vacanteDto);
    Task<GenericResponseDto<VacanteDto>> UpdateAsync(VacanteUpdateDto vacanteDto);
    Task<GenericResponseDto<bool>> DeleteAsync(int id);
    Task<GenericResponseDto<List<VacanteDto>>> GetByInstitucionAsync(int institucionId);
    Task<GenericResponseDto<List<VacanteDto>>> GetByCategoriaAsync(int categoriaId);
    Task<GenericResponseDto<List<VacanteDto>>> GetByProvinciaAsync(int provinciaId);
    Task<GenericResponseDto<bool>> ActivateAsync(int id);
    Task<GenericResponseDto<bool>> DeactivateAsync(int id);
    Task<GenericResponseDto<VacanteDto>> GetVacanteById(int VacanteId);
}
