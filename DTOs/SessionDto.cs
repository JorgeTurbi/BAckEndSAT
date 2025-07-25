using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs;

public class SessionDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string Token { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
}
