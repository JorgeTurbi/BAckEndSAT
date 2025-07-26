using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VacanteController : ControllerBase
{
    private readonly IVacanteService _vacanteService;

    public VacanteController(IVacanteService vacanteService)
    {
        _vacanteService = vacanteService;
    }

    /// <summary>
    /// Obtiene todas las vacantes
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<GenericResponseDto<List<VacanteDto>>>> GetAll()
    {
        var result = await _vacanteService.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    /// Obtiene solo las vacantes activas
    /// </summary>
    [HttpGet("active")]
    public async Task<ActionResult<GenericResponseDto<List<VacanteDto>>>> GetActive()
    {
        var result = await _vacanteService.GetActiveAsync();
        return Ok(result);
    }

    /// <summary>
    /// Obtiene una vacante por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<GenericResponseDto<VacanteDto>>> GetById(int id)
    {
        var result = await _vacanteService.GetByIdAsync(id);
        if (!result.Success)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// Obtiene vacantes por institución
    /// </summary>
    [HttpGet("institucion/{institucionId}")]
    public async Task<ActionResult<GenericResponseDto<List<VacanteDto>>>> GetByInstitucion(
        int institucionId
    )
    {
        var result = await _vacanteService.GetByInstitucionAsync(institucionId);
        return Ok(result);
    }

    /// <summary>
    /// Obtiene vacantes por categoría
    /// </summary>
    [HttpGet("categoria/{categoriaId}")]
    public async Task<ActionResult<GenericResponseDto<List<VacanteDto>>>> GetByCategoria(
        int categoriaId
    )
    {
        var result = await _vacanteService.GetByCategoriaAsync(categoriaId);
        return Ok(result);
    }

    /// <summary>
    /// Obtiene vacantes por provincia
    /// </summary>
    [HttpGet("provincia/{provinciaId}")]
    public async Task<ActionResult<GenericResponseDto<List<VacanteDto>>>> GetByProvincia(
        int provinciaId
    )
    {
        var result = await _vacanteService.GetByProvinciaAsync(provinciaId);
        return Ok(result);
    }

    /// <summary>
    /// Crea una nueva vacante
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<GenericResponseDto<VacanteDto>>> Create(
        [FromBody] VacanteCreateDto vacanteDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                new GenericResponseDto<VacanteDto>
                {
                    Success = false,
                    Message = "Datos inválidos",
                    Data = null,
                }
            );
        }

        var result = await _vacanteService.CreateAsync(vacanteDto);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
    }

    /// <summary>
    /// Activamos una vacante existente
    /// </summary>
    [HttpPatch("{id:int}/activate")]
    public async Task<ActionResult<GenericResponseDto<bool>>> Activate(int id)
    {
        var result = await _vacanteService.ActivateAsync(id);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Desactivamos una vacante existente
    /// </summary>
    [HttpPatch("{id:int}/deactivate")]
    public async Task<ActionResult<GenericResponseDto<bool>>> Deactivate(int id)
    {
        var result = await _vacanteService.DeactivateAsync(id);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Actualiza una vacante existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<GenericResponseDto<VacanteDto>>> Update(
        int id,
        [FromBody] VacanteUpdateDto vacanteDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                new GenericResponseDto<VacanteDto>
                {
                    Success = false,
                    Message = "Datos inválidos",
                    Data = null,
                }
            );
        }

        vacanteDto.Id = id;

        var result = await _vacanteService.UpdateAsync(vacanteDto);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Elimina una vacante
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<GenericResponseDto<bool>>> Delete(int id)
    {
        var result = await _vacanteService.DeleteAsync(id);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
