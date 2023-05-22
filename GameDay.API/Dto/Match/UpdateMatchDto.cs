namespace GameDay.API.Dto.Match
{
    public class UpdateMatchDto
    {
        /// <summary>
        /// Dès qu'un match un fini, dire que l'équipe local a soit gagné, perdu ou a fait égalité face à l'équipe adverse
        /// </summary>
        public bool WinMatch { get; set; }
        public bool LoseMatch { get; set; }
        public bool EgalMatch { get; set; }
    }
}
