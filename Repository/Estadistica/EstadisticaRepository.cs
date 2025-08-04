using BackEndSAT.Services.IEstadistica;
using Context;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace Repository.Estadistica;

public class EstadisticaRepository : IEstadisticaService
{
    private readonly ApplicationDbContext _Context;
    public EstadisticaRepository(ApplicationDbContext context)
    {
        _Context = context;
    }
    public async Task<DashboardDTO> Estadistica()
    {
        int usuarios = await _Context.Users.CountAsync();
        int vacanteList = await _Context.Vacantes.Where(a => a.IsActive).CountAsync();
        int Aplicaciones = await _Context.AplicacionVacantes.CountAsync();
        int Asignaciones = 0;
        return new DashboardDTO
        {
            UsuarioRegistrados = usuarios,
            VacantesActivas = vacanteList,
            TotalAplicaciones = Aplicaciones,
            TotalAsignaciones = Asignaciones
        };
    }
}