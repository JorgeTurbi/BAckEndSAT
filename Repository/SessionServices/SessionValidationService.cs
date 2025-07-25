using Context;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.SessionServices;

namespace Repository.SessionServices;

public class SessionValidationService : ISessionValidationService
{
    private readonly ApplicationDbContext _context;

    public SessionValidationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Session?> ValidateTokenAsync(string token)
    {
        var session = await _context
            .Sessions.Include(s => s.User)
            .FirstOrDefaultAsync(s =>
                s.Token == token && !s.IsRevoked && s.ExpiresAt > DateTime.UtcNow
            );

        // Si la sesión existe pero el usuario está inactivo, revocar la sesión
        if (session != null && session.User != null && !session.User.IsActive)
        {
            session.IsRevoked = true;
            session.RevokedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return null;
        }

        return session;
    }

    public async Task<bool> RevokeSessionAsync(string token)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s =>
            s.Token == token && !s.IsRevoked
        );

        if (session == null)
            return false;

        session.IsRevoked = true;
        session.RevokedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> RevokeAllUserSessionsAsync(int userId)
    {
        var activeSessions = await _context
            .Sessions.Where(s => s.UserId == userId && !s.IsRevoked)
            .ToListAsync();

        foreach (var session in activeSessions)
        {
            session.IsRevoked = true;
            session.RevokedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return activeSessions.Count;
    }
}
