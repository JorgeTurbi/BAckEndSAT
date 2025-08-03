
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Instituciones;

namespace BackEndSAT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class InstitucionController : ControllerBase
    {
        private readonly IInstitucion _institucionService;
        public InstitucionController(IInstitucion institucionService)
        {
            _institucionService = institucionService;
        }


        /// <summary>
        /// Endpoint para obtener todas las instituciones

        [HttpGet]

        public async Task<IActionResult> GetAllInstituciones() => Ok(await _institucionService.GetAllAsync());

    }
}