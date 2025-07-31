using BackEndSAT.DTOs;
using DTOs;

namespace Services.AplicacionesVacante;

public interface IAplicacionVacanteService
{
    Task<GenericResponseDto<bool>> CrearAsync(AplicacionVacanteDto aplicar);
    Task<GenericResponseDto<List<AplicacionesDTO>>> GetAllAsyncbyUserId(int UserId);
    // Task<AplicacionVacanteDto?> GetByIdAsync(int aplicacionId);

}