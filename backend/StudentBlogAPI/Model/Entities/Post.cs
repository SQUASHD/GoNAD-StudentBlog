using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentBlogAPI.Model.Entities;

public enum PublicationStatus
{
    Draft,
    Published
}

public class Post
{
    [Key] public int Id { get; set; }

    [ForeignKey("UserId")] public int UserId { get; set; }

    [Required] [MaxLength(128)] public string Title { get; set; } = string.Empty;

    [Required] [MaxLength(16777215)] public string Content { get; set; } = string.Empty;
    [Required] [MaxLength(32)] public PublicationStatus Status { get; set; } = PublicationStatus.Draft;

    [Required] public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required] public DateTime UpdatedAt { get; set; } = DateTime.Now;
}