using DTOs;

namespace Services.Perfil;

public interface IInterfacePerfil
{
    Task<GenericResponseDto<List<RangoDTO>>> GetRangos();
    Task<GenericResponseDto<List<InstitucionMilitarDTO>>> GetInstituciones();
}
