using System.ComponentModel.DataAnnotations;

namespace SpaceShooterHighscore
{
    public class Highscore
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Playername { get; set; }

        [Required]
        public int Points { get; set; }
    }
}
