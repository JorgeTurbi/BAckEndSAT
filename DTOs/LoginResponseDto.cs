namespace DTOs;

public class LoginResponseDto
{
    public required string Token { get; set; }
    public required DateTime ExpiresAt { get; set; }
    public required UserProfileDto User { get; set; }
    public ApplicanteDto? Perfil { get; set; } = null;
}
