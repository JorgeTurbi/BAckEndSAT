using Context;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ProvinciaRepository : IProvinciaRepository
{
    private readonly ApplicationDbContext _context;

    public ProvinciaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Provincia>> GetAllAsync()
    {
        return await _context.Provincias.OrderBy(p => p.Nombre).ToListAsync();
    }

    public async Task<Provincia?> GetByIdAsync(int id)
    {
        return await _context.Provincias.FirstOrDefaultAsync(p => p.Id == id);
    }
}
