using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Context;
using DTOs;
using Entities;
using Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;

namespace Respository;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _Context;
    private readonly IMapper _Mapper;
    private readonly IConfiguration _config;

    public AuthService(ApplicationDbContext context, IMapper mapper, IConfiguration config)
    {
        _Context = context;
        _Mapper = mapper;
        _config = config;
    }

    // This class is currently empty, but it can be used to implement authentication-related methods in the future.
    public async Task<GenericResponseDto<LoginResponseDto>> LoginAsync(
        LoginDto dto,
        string ipAddress,
        string userAgent
    )
    {
        // Buscar al usuario por nombre de usuario (puedes ajustar para buscar también por email, según tus necesidades)
        var user = await _Context
            .Users.Include(u => u.Institucion)
            .Include(u => u.Departamento)
            .FirstOrDefaultAsync(u => u.Usuario == dto.Usuario);

        if (user == null)
        {
            return new GenericResponseDto<LoginResponseDto>
            {
                Success = false,
                Message = "El usuario no existe",
                Data = null,
            };
        }

        if (user.IsActive == false)
        {
            return new GenericResponseDto<LoginResponseDto>
            {
                Success = false,
                Message = "Usuario Inactivo",
                Data = null,
            };
        }

        // Verificar la contraseña usando el PasswordHelper
        bool isValidPassword = PasswordHelper.VerifyPassword(user.PasswordHash, dto.Password);

        if (!isValidPassword)
        {
            return new GenericResponseDto<LoginResponseDto>
            {
                Success = false,
                Message = "Contraseña incorrecta",
                Data = null,
            };
        }

        // Generar el token JWT
        var tokenData = await GenerateTokenWithSession(user, ipAddress, userAgent);

        // Mapear usuario a UserProfileDto
        var userProfile = _Mapper.Map<UserProfileDto>(user);

        var loginResponse = new LoginResponseDto
        {
            Token = tokenData.Token,
            ExpiresAt = tokenData.ExpiresAt,
            User = userProfile,
        };

        return new GenericResponseDto<LoginResponseDto>
        {
            Success = true,
            Message = "Login exitoso",
            Data = loginResponse,
        };
    }

    public async Task<GenericResponseDto<UserProfileDto>> GetUserProfileAsync(int userId)
    {
        var user = await _Context
            .Users.Include(u => u.Institucion)
            .Include(u => u.Departamento)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return new GenericResponseDto<UserProfileDto>
            {
                Success = false,
                Message = "Usuario no encontrado",
                Data = null,
            };
        }

        var userProfile = _Mapper.Map<UserProfileDto>(user);

        return new GenericResponseDto<UserProfileDto>
        {
            Success = true,
            Message = "Perfil obtenido exitosamente",
            Data = userProfile,
        };
    }

    public async Task<GenericResponseDto<bool>> LogoutAsync(string token)
    {
        var session = await _Context.Sessions.FirstOrDefaultAsync(s =>
            s.Token == token && !s.IsRevoked
        );

        if (session == null)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Message = "Sesión no encontrada o ya revocada",
                Data = false,
            };
        }

        session.IsRevoked = true;
        session.RevokedAt = DateTime.UtcNow;

        try
        {
            await _Context.SaveChangesAsync();
            return new GenericResponseDto<bool>
            {
                Success = true,
                Message = "Logout exitoso",
                Data = true,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Message = ex.Message,
                Data = false,
            };
        }
    }

    public async Task<GenericResponseDto<bool>> LogoutAllSessionsAsync(int userId)
    {
        var activeSessions = await _Context
            .Sessions.Where(s => s.UserId == userId && !s.IsRevoked)
            .ToListAsync();

        foreach (var session in activeSessions)
        {
            session.IsRevoked = true;
            session.RevokedAt = DateTime.UtcNow;
        }

        try
        {
            await _Context.SaveChangesAsync();
            return new GenericResponseDto<bool>
            {
                Success = true,
                Message = $"Se cerraron {activeSessions.Count} sesiones activas",
                Data = true,
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Message = ex.Message,
                Data = false,
            };
        }
    }

    public async Task<GenericResponseDto<bool>> RegisterAsync(UserDto dto)
    {
        var existsCedula = await _Context.Users.AnyAsync(u => u.Cedula == dto.Cedula);
        if (existsCedula)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Data = false,
                Message = "La cédula ya está registrada.",
            };
        }

        var existsEmail = await _Context.Users.AnyAsync(u => u.Email == dto.Email);
        if (existsEmail)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Data = false,
                Message = "El correo electrónico ya está registrado.",
            };
        }

        var existsUsername = await _Context.Users.AnyAsync(u => u.Usuario == dto.Usuario);
        if (existsUsername)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Data = false,
                Message = "El nombre de usuario ya está en uso.",
            };
        }

        // Hash de la contraseña (puedes usar BCrypt u otra lib)
        var passwordHash = PasswordHelper.HashPassword(dto.PasswordHash);
        dto.PasswordHash = passwordHash; // Asignar el hash al DTO
        var user = _Mapper.Map<User>(dto);
        user.CreatedAt = DateTime.UtcNow;
        user.IsActive = false;
        // user.PasswordHash = passwordHash;
        user.Role = dto.UserType == "V" ? "Admin" : "User"; // Asignar rol basado en UserType

        await _Context.Users.AddAsync(user);

        try
        {
            await _Context.SaveChangesAsync();
            return new GenericResponseDto<bool>
            {
                Success = true,
                Data = true,
                Message = "Usuario registrado exitosamente.",
            };
        }
        catch (Exception ex)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Data = false,
                Message = ex.Message,
            };
        }
    }

    private async Task<(string Token, DateTime ExpiresAt)> GenerateTokenWithSession(
        User user,
        string ip,
        string userAgent
    )
    {
        var jwtSettings = _config.GetSection("Jwt");
        var secret = jwtSettings["Key"];

        if (string.IsNullOrEmpty(secret))
            throw new Exception("La clave JWT (Jwt:Key) no está configurada.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expirationMinutes = double.Parse(jwtSettings["ExpireMinutes"]!);
        var expiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Usuario),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: expiresAt,
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        // Crear la sesión en la base de datos
        var session = new Session
        {
            UserId = user.Id,
            Token = tokenString,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = expiresAt,
            IsRevoked = false,
            IpAddress = ip,
            UserAgent = userAgent,
        };

        await _Context.Sessions.AddAsync(session);
        await _Context.SaveChangesAsync();

        return (tokenString, expiresAt);
    }

    public async Task<GenericResponseDto<bool>> ActivarUsuario(int userId)
    {
        if (userId <= 0)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Message = "ID de usuario inválido",
                Data = false,
            };
        }
        var user = await _Context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Message = "Usuario no encontrado",
                Data = false,
            };
        }
        if (user.IsActive)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Message = "El usuario ya está activo",
                Data = false,
            };
        }

        user.IsActive = true;
        var respuesta = await _Context.SaveChangesAsync() > 0 ? true : false;
        return new GenericResponseDto<bool>
        {
            Success = respuesta,
            Message = respuesta ? "Usuario activado exitosamente" : "Error al activar el usuario",
            Data = respuesta,
        };
            
        
    }
}
