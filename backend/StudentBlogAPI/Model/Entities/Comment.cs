using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentBlogAPI.Model.Entities;

public class Comment
{
    [Key] public int Id { get; set; }

    [ForeignKey("PostId")] public int PostId { get; set; }

    [ForeignKey("UserId")] public int UserId { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(140)]
    public string Content { get; set; } = string.Empty;

    [Required] public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required] public DateTime UpdatedAt { get; set; } = DateTime.Now;
}