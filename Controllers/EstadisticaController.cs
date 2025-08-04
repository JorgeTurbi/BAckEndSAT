using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndSAT.Services.IEstadistica;
using Microsoft.AspNetCore.Mvc;

namespace BackEndSAT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadisticaController : ControllerBase
    {
        private readonly IEstadisticaService _service;
        public EstadisticaController(IEstadisticaService service)
        {
            _service = service;
        }

        [HttpGet("Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            return Ok(await _service.Estadistica());
        }
    }
}