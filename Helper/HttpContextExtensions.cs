namespace Helper;

public static class HttpContextExtensions
{
    public static string GetBearerToken(this HttpContext ctx)
    {
        var auth = ctx.Request.Headers["Authorization"].FirstOrDefault();
        if (
            string.IsNullOrWhiteSpace(auth)
            || !auth.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
        )
            return string.Empty;

        return auth["Bearer ".Length..].Trim();
    }

    public static string GetUserAgent(this HttpContext ctx) =>
        ctx.Request.Headers["User-Agent"].FirstOrDefault() ?? string.Empty;

    public static string GetClientIp(this HttpContext ctx)
    {
        // Si estás detrás de proxy/reverse-proxy, habilita ForwardedHeaders en Program.cs
        var ip = ctx.Connection.RemoteIpAddress?.ToString();

        // X-Forwarded-For: puede traer múltiples IP separadas por coma. Tomamos la primera.
        var xff = ctx.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrWhiteSpace(xff))
        {
            ip =
                xff.Split(
                        ',',
                        StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries
                    )
                    .FirstOrDefault() ?? ip;
        }

        return ip ?? string.Empty;
    }
}
