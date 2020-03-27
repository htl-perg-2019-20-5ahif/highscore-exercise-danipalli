using Microsoft.EntityFrameworkCore;
using SpaceShooterHighscore.Controllers.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SpaceShooterHighscore.Tests
{
    public class APITest
    {
        DbContextOptions<HighscoreDbContext> options;
        HighscoreDbContext dbContext;
        HighscoreManagement highscoreManagement;

        // These Tests must run on an empty database
        public APITest()
        {
            options = new DbContextOptionsBuilder<HighscoreDbContext>().UseSqlServer("Server=localhost;Database=Highscore;User Id=sa;Password=yourStrong(!)Password;").Options;
            dbContext = new HighscoreDbContext(options);
            dbContext.Database.Migrate();
            highscoreManagement = new HighscoreManagement(dbContext);
        }

        [Fact]
        public async void SimpleTestIfTestsWorkWithDatabaseConnectionString()
        {
            var highscores = (await highscoreManagement.GetHighscores());
            Assert.Empty(highscores);
        }

        [Fact]
        public async Task AddMoreThanTenHighscores()
        {
            Highscore highscore;

            int score = 100;

            while (score <= 1100)    // Will add the highscore eleven times
            {
                highscore = new Highscore()
                {
                    Playername = "Player One " + score,
                    Points = score
                };
                await highscoreManagement.AddHighscore(highscore);
                score += 100;
            }

            var highscores = await highscoreManagement.GetHighscores();

            Assert.True(highscores.Count == 10);
            Assert.True(highscores.Last().Points == 200);

            dbContext.Highscores.RemoveRange(highscores);
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task AddOneHighscore()
        {
            Highscore highscore = new Highscore()
            {
                Playername = "Player One",
                Points = 1200
            };

            await highscoreManagement.AddHighscore(highscore);

            var highscores = await highscoreManagement.GetHighscores();

            Assert.True(highscores.Count == 1);
            Assert.True(highscores.First().Playername.Equals("Player One"));
            Assert.True(highscores.First().Points == 1200);

            dbContext.Highscores.RemoveRange(highscores);
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task CheckCorrectReturnOrder()
        {
            Highscore highscore1 = new Highscore()
            {
                Playername = "Player One",
                Points = 1200
            };

            Highscore highscore2 = new Highscore()
            {
                Playername = "Player Two",
                Points = 1500
            };

            await highscoreManagement.AddHighscore(highscore1);
            await highscoreManagement.AddHighscore(highscore2);

            var highscores = await highscoreManagement.GetHighscores();

            Assert.True(highscores.Count == 2);
            Assert.True(highscores.First().Playername.Equals("Player Two"));
            Assert.True(highscores.Last().Playername.Equals("Player One"));

            dbContext.Highscores.RemoveRange(highscores);
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task AddMoreThanTenHighscoresAndTheLastEqualToLowest()
        {
            Highscore highscore;

            int score = 100;

            while (score <= 1100)    // Will add the highscore eleven times
            {
                highscore = new Highscore()
                {
                    Playername = "Player One " + score,
                    Points = score
                };
                await highscoreManagement.AddHighscore(highscore);
                score += 100;
            }

            highscore = new Highscore()
            {
                Playername = "Player Two 200",
                Points = 200
            };
            await highscoreManagement.AddHighscore(highscore);

            var highscores = await highscoreManagement.GetHighscores();

            Assert.True(highscores.Last().Playername.Equals("Player One 200"));

            dbContext.Highscores.RemoveRange(highscores);
            await dbContext.SaveChangesAsync();
        }
    }
}
