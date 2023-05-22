using GameDay.API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameDay.API.Dto.User
{
    public class CreateUserDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string LicenceNumber { get; set; }
        public int TeamId { get; set; }
    }
}
