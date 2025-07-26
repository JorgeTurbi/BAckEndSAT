using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoriaVacanteController : ControllerBase
{
    private readonly ICategoriaVacanteService _categoriaService;

    public CategoriaVacanteController(ICategoriaVacanteService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    /// <summary>
    /// Obtenemos todas las categorías de vacantes
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<GenericResponseDto<List<CategoriaVacanteDto>>>> GetAll()
    {
        var result = await _categoriaService.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    /// Obtiene una categoría por ID
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GenericResponseDto<CategoriaVacanteDto>>> GetById(int id)
    {
        var result = await _categoriaService.GetByIdAsync(id);
        if (!result.Success)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// Creamos una nueva categoría de vacante
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<GenericResponseDto<CategoriaVacanteDto>>> Create(
        [FromBody] CategoriaVacanteDto categoriaDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                new GenericResponseDto<CategoriaVacanteDto>
                {
                    Success = false,
                    Message = "Datos inválidos",
                    Data = null,
                }
            );
        }

        var result = await _categoriaService.CreateAsync(categoriaDto);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
    }

    /// <summary>
    /// Actualizamos una categoría existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<GenericResponseDto<CategoriaVacanteDto>>> Update(
        int id,
        [FromBody] CategoriaVacanteDto categoriaDto
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(
                new GenericResponseDto<CategoriaVacanteDto>
                {
                    Success = false,
                    Message = "Datos inválidos",
                }
            );

        categoriaDto.Id = id;

        var result = await _categoriaService.UpdateAsync(categoriaDto);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Eliminamos una categoría
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<GenericResponseDto<bool>>> Delete(int id)
    {
        var result = await _categoriaService.DeleteAsync(id);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
