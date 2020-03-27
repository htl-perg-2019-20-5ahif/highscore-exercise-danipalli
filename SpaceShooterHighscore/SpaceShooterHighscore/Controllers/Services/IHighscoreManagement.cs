using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaceShooterHighscore.Controllers.Services
{
    public interface IHighscoreManagement
    {
        public Task<bool> AddHighscore(Highscore highscore);
        public Task<List<Highscore>> GetHighscores();
    }
}
