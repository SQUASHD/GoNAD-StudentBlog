using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentBlogAPI.Model.Entities;

public class Post
{
    [Key] public int Id { get; set; }

    [ForeignKey("UserId")] public int UserId { get; set; }

    [Required] [MaxLength(128)] public string Title { get; set; } = string.Empty;

    [Required] [MaxLength(16777215)] public string Content { get; set; } = string.Empty;

    [Required] public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required] public DateTime UpdatedAt { get; set; } = DateTime.Now;
}