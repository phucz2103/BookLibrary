using BookLibrary.Domain;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User : IdentityUser<Guid>
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    public string? Avatar { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public override string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(256)]
    public override string UserName { get; set; } = string.Empty;

    [Required]
    public override string PasswordHash { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Address { get; set; }

    [Phone]
    public override string? PhoneNumber { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<BorrowOrder> borrowOrders { get; set; } = new List<BorrowOrder>();
}