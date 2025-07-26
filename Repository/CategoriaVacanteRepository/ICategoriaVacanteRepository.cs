using Entities;

namespace Repository;

public interface ICategoriaVacanteRepository
{
    Task<List<CategoriaVacante>> GetAllAsync();
    Task<CategoriaVacante?> GetByIdAsync(int id);
    Task<CategoriaVacante?> GetByNombreAsync(string nombre);
    Task<CategoriaVacante> CreateAsync(CategoriaVacante categoria);
    Task<CategoriaVacante> UpdateAsync(CategoriaVacante categoria);
    Task<bool> DeleteAsync(int id);
    Task<bool> HasVacantesAsync(int categoriaId);
}
