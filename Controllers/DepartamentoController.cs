using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Instituciones;

namespace BackEndSAT.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DepartamentoController : ControllerBase
{
    private readonly IInstitucion _institucionService;
    public DepartamentoController(IInstitucion institucionService)
    {
        _institucionService = institucionService;
    }

    [HttpGet]

    public async Task<IActionResult> GetAllDepartamentos() => Ok(await _institucionService.GetAllDepartamentosAsync());
}
