using Entities;

namespace Services.SessionServices;

public interface ISessionValidationService
{
    Task<Session?> ValidateTokenAsync(string token);
    Task<bool> RevokeSessionAsync(string token);
    Task<int> RevokeAllUserSessionsAsync(int userId);
}
