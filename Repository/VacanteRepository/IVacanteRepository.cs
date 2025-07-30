using DTOs;
using Entities;

namespace Repository;

public interface IVacanteRepository
{
    Task<GenericResponseDto<List<VacanteDto>>> GetAllWithDetailsAsync();
    Task<List<Vacante>> GetActiveWithDetailsAsync();
    Task<Vacante?> GetByIdAsync(int id);
    Task<Vacante?> GetByIdWithDetailsAsync(int id);
    Task<GenericResponseDto<bool>> CreateAsync(VacanteCreateDto vacante);
    Task<Vacante> UpdateAsync(Vacante vacante);
    Task<bool> DeleteAsync(int id);
    Task<List<Vacante>> GetByInstitucionWithDetailsAsync(int institucionId);
    Task<List<Vacante>> GetByCategoriaWithDetailsAsync(int categoriaId);
    Task<List<Vacante>> GetByProvinciaWithDetailsAsync(int provinciaId);
    Task<bool> InstitucionExistsAsync(int institucionId);
    Task<bool> ProvinciaExistsAsync(int provinciaId);
    Task<bool> CategoriaExistsAsync(int categoriaId);
    Task<bool> SetActiveAsync(int id, bool active);
}
