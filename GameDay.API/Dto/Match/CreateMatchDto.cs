namespace GameDay.API.Dto.Match
{
    public class CreateMatchDto
    {
        public DateTime DateMatch { get; set; }
        /// <summary>
        /// Heure début du match
        /// </summary>
        public DateTime TimeDebMatch { get; set; }
        /// <summary>
        /// Heure fin de match
        /// </summary>
        public DateTime TimeFinMatch { get; set; }
        public bool EgalMatch { get; set; }
        public bool WinMatch { get; set; }
        public bool LoseMatch { get; set; }
    }
}
