using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentBlogAPI.Model.Entities;

[Index(nameof(Token), IsUnique = true)]
public class RevokedToken
{
    [Key] public int Id { get; set; }
    [Required] public string Token { get; set; } = string.Empty;
    [Required] public DateTime CreatedAt { get; set; } = DateTime.Now;
    [ForeignKey("UserId")] public int UserId { get; set; }
}