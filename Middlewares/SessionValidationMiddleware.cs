using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Services.SessionServices;

namespace BackEndSAT.Middlewares;

public class SessionValidationMiddleware
{
    private readonly RequestDelegate _next;

    public SessionValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext ctx)
    {
        // Resolver el servicio scoped desde el contexto de la petición
        var validator = ctx.RequestServices.GetRequiredService<ISessionValidationService>();

        // ---- archivos estáticos o docs ----------
        var path = ctx.Request.Path.Value!.ToLowerInvariant();
        if (
            path.StartsWith("/swagger")
            || path.StartsWith("/openapi")
            || path.StartsWith("/redoc")
            || path.StartsWith("/_framework")
            || path.StartsWith("/css")
            || path.StartsWith("/js")
            || path.StartsWith("/images")
        )
        {
            await _next(ctx); // se sirve sin validar token
            return;
        }

        // End-points públicos (con AllowAnonymous)
        var endPoint = ctx.GetEndpoint();
        if (endPoint?.Metadata.GetMetadata<IAllowAnonymous>() is not null)
        {
            await _next(ctx);
            return;
        }

        // Verificar si el endpoint requiere autorización
        var requiresAuth = endPoint?.Metadata.GetMetadata<AuthorizeAttribute>() is not null;
        if (!requiresAuth)
        {
            await _next(ctx);
            return;
        }

        var auth = ctx.Request.Headers["Authorization"].FirstOrDefault();
        if (
            string.IsNullOrWhiteSpace(auth)
            || !auth.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
        )
        {
            ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
            ctx.Response.ContentType = "application/json";
            await ctx.Response.WriteAsync(
                """
                {
                    "success": false,
                    "message": "Token de autorización requerido",
                    "data": null
                }
                """
            );
            return;
        }

        var token = auth["Bearer ".Length..].Trim();
        var session = await validator.ValidateTokenAsync(token);

        if (session is null)
        {
            ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
            ctx.Response.ContentType = "application/json";
            await ctx.Response.WriteAsync(
                """
                {
                    "success": false,
                    "message": "Sesión inválida o expirada",
                    "data": null
                }
                """
            );
            return;
        }

        // Creamos identidad con claims del usuario
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, session.UserId.ToString()),
            new(ClaimTypes.Name, session.User?.Usuario ?? ""),
            new(ClaimTypes.Email, session.User?.Email ?? ""),
            new(ClaimTypes.Role, session.User?.Role ?? ""),
            new("SessionToken", session.Token),
            new("SessionId", session.Id.ToString()),
        };

        var identity = new ClaimsIdentity(claims, "SessionToken");
        if (ctx.User?.Identity?.IsAuthenticated == true)
        {
            // Agregas la identidad extra con SessionId/SessionToken
            ((ClaimsPrincipal)ctx.User).AddIdentity(identity);
        }
        else
        {
            ctx.User = new ClaimsPrincipal(identity);
        }

        await _next(ctx);
    }
}
