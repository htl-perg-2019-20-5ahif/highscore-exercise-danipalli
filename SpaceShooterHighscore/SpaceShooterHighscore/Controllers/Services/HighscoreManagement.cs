using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceShooterHighscore.Controllers.Services
{
    public class HighscoreManagement : IHighscoreManagement
    {
        private HighscoreDbContext _context;

        public HighscoreManagement(HighscoreDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddHighscore(Highscore highscore)
        {
            var highscores = await _context.Highscores.OrderByDescending(h => h.Points).ToListAsync();
            if (highscores.Count >= 10 && highscore.Points <= highscores.Last().Points)
            {
                return false;
            }

            _context.Highscores.Add(highscore);
            if (highscores.Count >= 10)
            {
                _context.Highscores.Remove(highscores.Last());
            }
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Highscore>> GetHighscores()
        {
            return await _context.Highscores.OrderByDescending(h => h.Points).ToListAsync();
        }
    }
}
