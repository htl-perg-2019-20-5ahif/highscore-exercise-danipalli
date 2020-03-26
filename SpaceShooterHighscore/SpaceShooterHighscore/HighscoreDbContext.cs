using Microsoft.EntityFrameworkCore;

namespace SpaceShooterHighscore
{
    public class HighscoreDbContext : DbContext
    {
        public HighscoreDbContext(DbContextOptions<HighscoreDbContext> options) : base(options)
        { }

        public DbSet<Highscore> Highscores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
