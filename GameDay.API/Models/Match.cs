using System.ComponentModel.DataAnnotations.Schema;

namespace GameDay.API.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        /// <summary>
        /// Heure début du match
        /// </summary>
        public DateTime TimeDebMatch { get; set; }
        /// <summary>
        /// Heure fin de match
        /// </summary>
        public DateTime TimeFinMatch { get; set; }
        /// <summary>
        /// Nom de l'équipe de l'adversaire
        /// </summary>
        /// 
        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public Team Team { get; set; }
        public bool WinMatch { get; set; }
        public bool LoseMatch { get; set; }
        public bool EgalMatch { get; set; }
    }
}
