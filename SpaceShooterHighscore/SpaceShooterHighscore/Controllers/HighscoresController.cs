using Microsoft.AspNetCore.Mvc;
using SpaceShooterHighscore.Controllers.Services;
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
        private readonly IHighscoreManagement _highscoreManagement;

        public HighscoresController(HighscoreDbContext context, IHighscoreManagement highscoreManagement)
        {
            _context = context;
            _highscoreManagement = highscoreManagement;
        }

        // GET: api/Highscores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Highscore>>> GetHighscores()
        {
            return await _highscoreManagement.GetHighscores();
        }

        // POST: api/Highscores
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Highscore>> PostHighscore(Highscore highscore)
        {
            if (await _highscoreManagement.AddHighscore(highscore))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        private bool HighscoreExists(int id)
        {
            return _context.Highscores.Any(e => e.ID == id);
        }
    }
}
