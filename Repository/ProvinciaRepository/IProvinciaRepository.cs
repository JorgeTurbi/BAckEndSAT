using Entities;

namespace Repository;

public interface IProvinciaRepository
{
    Task<List<Provincia>> GetAllAsync();
    Task<Provincia?> GetByIdAsync(int id);
}
