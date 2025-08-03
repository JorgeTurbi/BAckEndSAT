using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProvinciaController : ControllerBase
{
    private readonly IProvinciaService _provinciaService;

    public ProvinciaController(IProvinciaService provinciaService)
    {
        _provinciaService = provinciaService;
    }

    /// <summary>
    /// Obtenemos todas las provincias
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<GenericResponseDto<List<ProvinciaDto>>>> GetAll()
    {
        var result = await _provinciaService.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    /// Obtenemos una provincia por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<GenericResponseDto<ProvinciaDto>>> GetById(int id)
    {
        var result = await _provinciaService.GetByIdAsync(id);
        if (!result.Success)
        {
            return NotFound(result);
        }
        return Ok(result);
    }
}
