using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Perfil;

namespace BackEndSAT.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GenericoController : ControllerBase
{
    private readonly IInterfacePerfil _Perfil;
    public GenericoController(IInterfacePerfil perfil)
    {
        _Perfil = perfil;
    }


    [Authorize]
    [HttpGet("GetInstitucionesMilitares")]
    public async Task<IActionResult> GetInstitucionesMilitares()
    {
        return Ok(await _Perfil.GetInstituciones());
    }


    [Authorize]
    [HttpGet("GetRangos")]
    public async Task<IActionResult> GetRangos()
    {
        return Ok(await _Perfil.GetRangos());
    }


}
