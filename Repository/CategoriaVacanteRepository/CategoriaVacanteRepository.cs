using Context;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class CategoriaVacanteRepository : ICategoriaVacanteRepository
{
    private readonly ApplicationDbContext _context;

    public CategoriaVacanteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoriaVacante>> GetAllAsync()
    {
        return await _context.CategoriasVacante.OrderBy(c => c.Nombre).ToListAsync();
    }

    public async Task<CategoriaVacante?> GetByIdAsync(int id)
    {
        return await _context.CategoriasVacante.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<CategoriaVacante?> GetByNombreAsync(string nombre)
    {
        return await _context.CategoriasVacante.FirstOrDefaultAsync(c =>
            c.Nombre.ToLower() == nombre.ToLower()
        );
    }

    public async Task<CategoriaVacante> CreateAsync(CategoriaVacante categoria)
    {
        _context.CategoriasVacante.Add(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<CategoriaVacante> UpdateAsync(CategoriaVacante categoria)
    {
        _context.CategoriasVacante.Update(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var categoria = await GetByIdAsync(id);
        if (categoria == null)
            return false;

        _context.CategoriasVacante.Remove(categoria);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> HasVacantesAsync(int categoriaId)
    {
        return await _context.Vacantes.AnyAsync(v => v.CategoriaId == categoriaId);
    }
}
