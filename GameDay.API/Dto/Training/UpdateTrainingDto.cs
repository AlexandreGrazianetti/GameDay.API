namespace GameDay.API.Dto.Training
{
    public class UpdateTrainingDto
    {
        /// <summary>
        /// Pour dire si un match est annulé ou maintenu
        /// </summary>
        public bool AnnulMain { get; set; }
        public DateTime DateTrain { get; set; }
        //Début de l'heure de l'entraînement
        public DateTime TimeDebTrain { get; set; }
        //Fin de l'entraînement
        public DateTime TimeEndTrain { get; set; }
    }
}
