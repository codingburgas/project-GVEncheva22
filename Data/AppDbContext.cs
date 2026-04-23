using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project_GVEncheva22.Models;

namespace project_GVEncheva22.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tables for boards and task items in the database.
        public DbSet<Board> Boards { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ApplicationUser -> Board (1-M): user owns boards and delete cascades.
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Boards)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Board -> TaskItem (1-M): board owns task items and delete cascades.
            builder.Entity<Board>()
                .HasMany(b => b.TaskItems)
                .WithOne(t => t.Board)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure field requirements and storage rules.
            builder.Entity<Board>(entity =>
            {
                entity.Property(x => x.Title).IsRequired().HasMaxLength(100);
            });

            builder.Entity<TaskItem>(entity =>
            {
                entity.Property(x => x.Title).IsRequired().HasMaxLength(200);
                entity.Property(x => x.Description).HasMaxLength(1000);
                entity.Property(x => x.Deadline).HasColumnType("datetime");
            });
        }
    }
}