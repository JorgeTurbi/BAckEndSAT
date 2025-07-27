using System.Security.Claims;
using DTOs;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.SessionServices;

namespace BackEndSAT.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ISessionValidationService _sessionService;

    public AuthController(IAuthService authService, ISessionValidationService sessionService)
    {
        _authService = authService;
        _sessionService = sessionService;
    }

    /// <summary>
    /// Endpoint para registrar un nuevo usuario
    /// </summary>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserDto dto)
    {
        var result = await _authService.RegisterAsync(dto);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    /// <summary>
    /// Endpoint para login
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var ip = HttpContext.GetClientIp();
        var userAgent = HttpContext.GetUserAgent();

        var result = await _authService.LoginAsync(dto, ip, userAgent);

        // if (!result.Success)
        //     return Unauthorized(result); // 401 si falla

        return Ok(result); // 200 con token si éxito
    }

    /// <summary>
    /// Endpoint para obtener el perfil del usuario autenticado
    /// </summary>
    /// <returns>Perfil completo del usuario</returns>
    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        {
            return Unauthorized(
                new GenericResponseDto<object>
                {
                    Success = false,
                    Message = "Token inválido o usuario no autenticado",
                    Data = null,
                }
            );
        }

        var result = await _authService.GetUserProfileAsync(userId);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    /// <summary>
    /// Endpoint para hacer logout (cerrar sesión actual)
    /// </summary>
    /// <returns>Confirmación del logout</returns>
    [HttpPost("ActivarUsuario")]
    public async Task<IActionResult> ActivarUsuario([FromQuery] int userId)
    {
        var result = await _authService.ActivarUsuario(userId);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        var auth = Request.Headers["Authorization"].FirstOrDefault();

        if (
            string.IsNullOrWhiteSpace(auth)
            || !auth.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
        )
        {
            return BadRequest(
                new GenericResponseDto<bool>
                {
                    Success = false,
                    Message = "Token de sesión no encontrado en el header Authorization",
                    Data = false,
                }
            );
        }

        var token = auth["Bearer ".Length..].Trim();

        var success = await _sessionService.RevokeSessionAsync(token);

        return Ok(
            new GenericResponseDto<bool>
            {
                Success = success,
                Message = success ? "Logout exitoso" : "Sesión no encontrada o ya revocada",
                Data = success,
            }
        );
    }

    /// <summary>
    /// Endpoint para cerrar todas las sesiones del usuario autenticado
    /// </summary>
    /// <returns>Confirmación del logout de todas las sesiones</returns>
    [HttpPost("logout-all")]
    [Authorize]
    public async Task<IActionResult> LogoutAllSessions()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        {
            return Unauthorized(
                new GenericResponseDto<bool>
                {
                    Success = false,
                    Message = "Token inválido o usuario no autenticado",
                    Data = false,
                }
            );
        }

        var revokedCount = await _sessionService.RevokeAllUserSessionsAsync(userId);

        return Ok(
            new GenericResponseDto<object>
            {
                Success = true,
                Message = $"Se cerraron {revokedCount} sesiones activas",
                Data = new { RevokedSessions = revokedCount },
            }
        );
    }

    /// <summary>
    /// Endpoint para obtener información básica de la sesión actual
    /// </summary>
    /// <returns>Información de la sesión</returns>
    [HttpGet("session-info")]
    [Authorize]
    public IActionResult GetSessionInfo()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        var sessionId = User.FindFirst("SessionId")?.Value;

        return Ok(
            new GenericResponseDto<object>
            {
                Success = true,
                Message = "Información de la sesión actual",
                Data = new
                {
                    UserId = userId,
                    Username = username,
                    Role = role,
                    SessionId = sessionId,
                    IsAuthenticated = true,
                    ServerTime = DateTime.UtcNow,
                },
            }
        );
    }
}
