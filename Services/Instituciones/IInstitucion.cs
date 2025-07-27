
using DTOs;
using Entities;

namespace Services.Instituciones;

public interface IInstitucion
{
    Task<GenericResponseDto<List<InstitucionDto>>> GetAllAsync();
  Task<GenericResponseDto<List<DepartamentoDto>>> GetAllDepartamentosAsync();

}