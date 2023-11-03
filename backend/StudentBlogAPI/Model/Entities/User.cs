using System.ComponentModel.DataAnnotations;
using StudentBlogAPI.Validation.UserValidator;

namespace StudentBlogAPI.Model.Entities;

public class User
{
    [Key] public int Id { get; set; }

    [Required]
    [UniqueUsername]
    [MinLength(3)]
    [MaxLength(20)]
    public string UserName { get; set; } = string.Empty;

    [Required] [MaxLength(64)] public string FirstName { get; set; } = string.Empty;

    [Required] [MaxLength(64)] public string LastName { get; set; } = string.Empty;

    [Required] [MaxLength(256)] public string HashedPassword { get; set; } = string.Empty;

    [Required] [MaxLength(256)] public string Salt { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [UniqueEmail]
    public string Email { get; set; } = string.Empty;

    [Required] public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required] public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [Required] public bool IsAdmin { get; set; }
}