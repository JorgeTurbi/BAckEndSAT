using BackEndSAT.DTOs;
using DTOs;

namespace Services.AplicacionesVacante;

public interface IAplicacionVacanteService
{
    Task<GenericResponseDto<bool>> CrearAsync(AplicacionVacanteDto aplicar);
    Task<GenericResponseDto<List<AplicacionesDTO>>> GetAllAsyncbyUserId(int UserId);
    Task<GenericResponseDto<List<AplicacionesDTO>>> GetAllAsync();
    Task<GenericResponseDto<List<AplicanteReclutadorDTO>>> ConsultaAplicantes(int VacanteId);
    Task<GenericResponseDto<List<FiltroVacanteDto>>> GetVacanteCategoriasActivas();
    // Task<AplicacionVacanteDto?> GetByIdAsync(int aplicacionId);

}