using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.AplicacionesVacante;

namespace BackEndSAT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AplicacionesController : ControllerBase
    {
        private readonly IAplicacionVacanteService _aplicarvacante;
        public AplicacionesController(IAplicacionVacanteService aplicarvacante)
        {
            _aplicarvacante = aplicarvacante;
        }


        [HttpPost("CrearAplicacion")]
        public async Task<IActionResult> CrearAplicacion([FromBody] AplicacionVacanteDto aplica)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Success = false, Message = "Datos inválidos" });
            return Ok(await _aplicarvacante.CrearAsync(aplica));
        }

        [HttpGet("GetAplicacionesbyUserId")]
        public async Task<IActionResult> GetAplicacionesbyUserId([FromQuery] int UserId)
        {

            return Ok(await _aplicarvacante.GetAllAsyncbyUserId(UserId));
        }
          [HttpGet("GetAplicaciones")]
        public async Task<IActionResult> GetAplicaciones()
        {

            return Ok(await _aplicarvacante.GetAllAsync());
        }

        [HttpGet("ConsultadeAplicantes")]
        public async Task<IActionResult> ConsultaAplicantes([FromQuery] int VacanteId)
        {

            return Ok(await _aplicarvacante.ConsultaAplicantes(VacanteId));
        }
    [HttpGet("GetVacanteCategoriasActivas")]
        public async Task<IActionResult> GetVacanteCategoriasActivas()
        {

            return Ok(await _aplicarvacante.GetVacanteCategoriasActivas());
        }



    }
}