using System.ComponentModel.DataAnnotations.Schema;

namespace GameDay.API.Models
{
    public class Training
    {
        public int TrainingId { get; set; }
        public DateTime DateTrain { get; set; }
        //Début de l'heure de l'entraînement
        public DateTime TimeDebTrain { get; set; }
        //Fin de l'entraînement
        public DateTime TimeEndTrain { get; set; }
        public bool AnnulMain { get; set; }
        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public Team Team { get; set; }
    }
}
