using DTOs;

namespace Services;

public interface IAuthService
{
    Task<GenericResponseDto<bool>> RegisterAsync(UserDto dto);
    Task<GenericResponseDto<LoginResponseDto>> LoginAsync(
        LoginDto dto,
        string ipAddress,
        string userAgent
    );
    Task<GenericResponseDto<UserProfileDto>> GetUserProfileAsync(int userId);
    Task<GenericResponseDto<bool>> LogoutAsync(string token);
    Task<GenericResponseDto<bool>> LogoutAllSessionsAsync(int userId);
    Task<GenericResponseDto<bool>> ActivarUsuario(int userId);
}
