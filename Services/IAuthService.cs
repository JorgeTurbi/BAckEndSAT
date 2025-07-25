
using DTOs;
namespace Services;
public interface IAuthService
{
    Task<GenericResponseDto<bool>> RegisterAsync(UserDto dto);
    Task<GenericResponseDto<string>> LoginAsync(LoginDto dto);
}
