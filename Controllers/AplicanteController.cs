using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Perfil;

namespace BackEndSAT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AplicanteController : ControllerBase
    {
        private readonly IUserProfileService _Service;
        public AplicanteController(IUserProfileService service)
        {
            _Service = service;
        }

        /// <summary>
    /// Crear un perfil de usuario (Aplicante)
    /// </summary>
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] ApplicanteDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { Success = false, Message = "Datos inválidos" });

            return Ok(await _Service.CreateAsync(dto));
    }

    /// <summary>
    /// Obtener perfil por ID
    /// </summary>
    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        var result = await _Service.GetByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
    }
}