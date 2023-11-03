using Microsoft.EntityFrameworkCore;
using StudentBlogAPI.Model.Entities;

namespace StudentBlogAPI.Data;

public class StudentBlogDbContext : DbContext
{
    public StudentBlogDbContext(DbContextOptions<StudentBlogDbContext> options) : base(options)
    {
    }


    public required DbSet<User> Users { get; set; }
    public required DbSet<Post> Posts { get; set; }
    public required DbSet<Comment> Comments { get; set; }
    public required DbSet<RevokedToken> RevokedTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Post>()
            .Property(e => e.Status)
            .HasConversion<string>();
    }
}