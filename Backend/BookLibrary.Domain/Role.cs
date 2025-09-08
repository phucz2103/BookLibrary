using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Role : IdentityRole<Guid>
{
    [Required]
    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;
}