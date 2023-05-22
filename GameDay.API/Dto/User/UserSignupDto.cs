namespace GameDay.API.Dto.User
{
    public class UserSignupDto
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? LicenceNumber { get; set; }
        public string Role { get; set; }
        public int TeamId { get; set; }
    }
}
