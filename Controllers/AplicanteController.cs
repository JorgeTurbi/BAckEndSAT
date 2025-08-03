using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Perfil;

namespace BackEndSAT.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
        public async Task<IActionResult> Create([FromBody] ApplicanteDto crearPerfilMilitar)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Success = false, Message = "Datos inválidos" });

            return Ok(await _Service.CreateAsync(crearPerfilMilitar));
        }

        /// <summary>
        /// Obtener perfil por ID
        /// </summary>
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int UserId)
        {
            var result = await _Service.GetByIdAsync(UserId);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}