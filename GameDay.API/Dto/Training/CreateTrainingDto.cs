namespace GameDay.API.Dto.Training
{
    public class CreateTrainingDto
    {
        /// <summary>
        /// Quand on souhaite créer un entraînement
        /// </summary>
        public DateTime DateTrain { get; set; }
        //Début de l'heure de l'entraînement
        public DateTime TimeDebTrain { get; set; }
        //Fin de l'entraînement
        public DateTime TimeEndTrain { get; set; }
    }
}
