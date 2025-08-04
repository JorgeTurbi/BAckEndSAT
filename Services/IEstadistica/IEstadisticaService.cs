using DTOs;

namespace BackEndSAT.Services.IEstadistica;

public interface IEstadisticaService
{
    Task<DashboardDTO> Estadistica();
}