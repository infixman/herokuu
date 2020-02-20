using Microsoft.EntityFrameworkCore;

namespace AspNetCoreDemoApp.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<JapanRestaurant> JapanRestaurant { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JapanRestaurant>().HasNoKey();
        }
    }
}
