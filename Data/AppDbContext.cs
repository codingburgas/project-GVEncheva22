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

        public DbSet<Board> Boards { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ApplicationUser -> Board (1-M)
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Boards)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Board -> TaskItem (1-M)
            builder.Entity<Board>()
                .HasMany(b => b.TaskItems)
                .WithOne(t => t.Board)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure required fields and lengths via fluent API as backup
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