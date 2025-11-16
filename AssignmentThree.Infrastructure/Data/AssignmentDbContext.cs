using Microsoft.EntityFrameworkCore;
using AssignmentThree.Core.Models;

namespace AssignmentThree.Infrastructure.Data;


public class AssignmentDbContext : DbContext
{
    public AssignmentDbContext (DbContextOptions<AssignmentDbContext> options) : base(options) {}

        public DbSet<Post> Posts {get; set;}
        public DbSet<Comment> Comments {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(p => p.Content);

            entity.Property(p => p.Author);

            entity.Property(p => p.CreatedDate);

            entity.Property(p => p.UpdatedDate);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.PostId)
                .IsRequired();

            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(1000);

            entity.Property(c => c.CreatedDate);

            entity.HasOne(p => p.Post)
                .WithMany(c => c.Comments)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.Cascade);

        });
    }
    
} 