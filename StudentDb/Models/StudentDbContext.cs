using Microsoft.EntityFrameworkCore;

namespace StudentDb.Models
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Students> Students { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Result>()
                .Property(r => r.TotalMarks)
                .HasComputedColumnSql("[Hindi] + [English] + [Science] + [History] + [GK] PERSISTED");
        }
    }
}
