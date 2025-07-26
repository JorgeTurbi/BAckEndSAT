using AutoMapper;
using DTOs;
using Repository;

namespace Services;

public class ProvinciaService : IProvinciaService
{
    private readonly IProvinciaRepository _provinciaRepository;
    private readonly IMapper _mapper;

    public ProvinciaService(IProvinciaRepository provinciaRepository, IMapper mapper)
    {
        _provinciaRepository = provinciaRepository;
        _mapper = mapper;
    }

    public async Task<GenericResponseDto<List<ProvinciaDto>>> GetAllAsync()
    {
        try
        {
            var provincias = await _provinciaRepository.GetAllAsync();
            var provinciasDto = _mapper.Map<List<ProvinciaDto>>(provincias);

            return new GenericResponseDto<List<ProvinciaDto>>
            {
                Success = true,
                Message = "Provincias obtenidas exitosamente",
                Data = provinciasDto,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<List<ProvinciaDto>>
            {
                Success = false,
                Message = $"Error al obtener provincias: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<GenericResponseDto<ProvinciaDto>> GetByIdAsync(int id)
    {
        try
        {
            var provincia = await _provinciaRepository.GetByIdAsync(id);
            if (provincia == null)
            {
                return new GenericResponseDto<ProvinciaDto>
                {
                    Success = false,
                    Message = "Provincia no encontrada",
                    Data = null,
                };
            }

            var provinciaDto = _mapper.Map<ProvinciaDto>(provincia);

            return new GenericResponseDto<ProvinciaDto>
            {
                Success = true,
                Message = "Provincia obtenida exitosamente",
                Data = provinciaDto,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<ProvinciaDto>
            {
                Success = false,
                Message = $"Error al obtener provincia: {ex.Message}",
                Data = null,
            };
        }
    }
}
