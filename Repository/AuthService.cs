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
    public async Task<GenericResponseDto<string>> LoginAsync(LoginDto dto)
    {


     // Buscar al usuario por nombre de usuario (puedes ajustar para buscar también por email, según tus necesidades)
    var user = await _Context.Users
        .FirstOrDefaultAsync(u => u.Usuario == dto.Usuario);

    if (user == null)
    {
        return new GenericResponseDto<string>
        {
            Success = false,
            Message = "El usuario no existe",
            Data = null
        };
    }
    if (user.IsActive == false)
    {
        return new GenericResponseDto<string>
        {
            Success = false,
            Message = "Usuario Inactivo",
            Data = null
        };
    }

    // Verificar la contraseña usando el PasswordHelper
        bool isValidPassword = PasswordHelper.VerifyPassword(user.PasswordHash, dto.Password);
    if (!isValidPassword)
    {
        return new GenericResponseDto<string>
        {
            Success = false,
            Message = "Contraseña incorrecta",
            Data = null
        };
    }

    // Generar el token JWT
    string token = GenerateToken(user);

    return new GenericResponseDto<string>
    {
        Success = true,
        Message = "Login exitoso",
        Data = token
    };
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
                Message = "La cédula ya está registrada."
            };
        }

        var existsEmail = await _Context.Users.AnyAsync(u => u.Email == dto.Email);
        if (existsEmail)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Data = false,
                Message = "El correo electrónico ya está registrado."
            };
        }

        var existsUsername = await _Context.Users.AnyAsync(u => u.Usuario == dto.Usuario);
        if (existsUsername)
        {
            return new GenericResponseDto<bool>
            {
                Success = false,
                Data = false,
                Message = "El nombre de usuario ya está en uso."
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
                Message = "Usuario registrado exitosamente."
            };
        }
        catch (Exception ex)
        {

            return new GenericResponseDto<bool>
            {
                Success = false,
                Data = false,
                Message = ex.Message
            };
        }



    }

private string GenerateToken(User user)
{
 var jwtSettings = _config.GetSection("Jwt");

    var secret = jwtSettings["Key"];
    if (string.IsNullOrEmpty(secret))
        throw new Exception("La clave JWT (Jwt:Key) no está configurada.");

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

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
        expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"]!)),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}
}