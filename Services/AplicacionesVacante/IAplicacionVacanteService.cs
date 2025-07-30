using DTOs;

namespace Services.AplicacionesVacante;

public interface IAplicacionVacanteService
{
    Task<GenericResponseDto<bool>> CrearAsync(AplicacionVacanteDto aplicar);
    // Task<List<AplicacionVacanteDto>> GetAllAsync();
    // Task<AplicacionVacanteDto?> GetByIdAsync(int aplicacionId);

}