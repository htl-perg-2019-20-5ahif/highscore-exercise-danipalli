using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceShooterHighscore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HighscoresController : ControllerBase
    {
        private readonly HighscoreDbContext _context;

        public HighscoresController(HighscoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Highscores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Highscore>>> GetHighscores()
        {
            return await _context.Highscores.OrderBy(h => h.Points).ToListAsync();
        }

        // POST: api/Highscores
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Highscore>> PostHighscore(Highscore highscore)
        {
            var highscores = await _context.Highscores.OrderByDescending(h => h.Points).ToListAsync();
            if (highscores.Count >= 10 && highscore.Points <= highscores.Last().Points)
            {
                return BadRequest();
            }

            _context.Highscores.Add(highscore);
            _context.Highscores.Remove(highscores.Last());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHighscore", new { id = highscore.ID }, highscore);
        }

        private bool HighscoreExists(int id)
        {
            return _context.Highscores.Any(e => e.ID == id);
        }
    }
}
