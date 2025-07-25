using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Session
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public required string Token { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; } = false;
    public DateTime? RevokedAt { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }

    // Navigation property
    public virtual User? User { get; set; }
}
