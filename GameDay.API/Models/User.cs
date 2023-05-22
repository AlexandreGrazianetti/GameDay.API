using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameDay.API.Models
{
    public class User : IdentityUser<Guid>
    {
        /// <summary>
        /// Numéro de licence de l'adhérent du club
        /// </summary>

        public string Name { get; set; }
        public string NickName { get; set; }
        public string? LicenceNumber { get; set; }
        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public Team Team { get; set; }
    }
}
