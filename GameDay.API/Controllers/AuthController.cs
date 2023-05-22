using GameDay.API.Dto.User;
using GameDay.API.Models;
using GameDay.API.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameDay.API.Controllers
{
    [ApiController]
    [Route("auth")]
    
    public class AuthControllers : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IOptionsSnapshot<JwtSettings> _jwtSettings;
         

        public AuthControllers(UserManager<User> userManager, RoleManager<Role> roleManager, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings;
        }
        [HttpPost("signup")]

        public async Task<IActionResult> SignUp([FromBody] UserSignupDto userSignUpDto)
        {
            var user = new User
            {
                Email = userSignUpDto.Email,
                UserName = userSignUpDto.Email,
                Name = userSignUpDto.Name,
                NickName = userSignUpDto.NickName,
                LicenceNumber = userSignUpDto.LicenceNumber,
                TeamId = userSignUpDto.TeamId,
            };

            var userCreateResult = await _userManager.CreateAsync(user, userSignUpDto.Password);

            if (userCreateResult.Succeeded)
            {
                var result = await _userManager.AddToRoleAsync(user, userSignUpDto.Role);

                if (result.Succeeded)
                {
                    return Created(string.Empty, string.Empty);
                }               
            }
            return Problem(userCreateResult.Errors.First().Description, null, 400);
        }
        [HttpPost("login")]

        public async Task<IActionResult> LogIn([FromBody] UserLoginDto userLoginDto)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == userLoginDto.Email);

            if (user is null)
            {
                return NotFound("L'utilisateur n'existe pas.");
            }

            var userLogInResult = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);

            if (userLogInResult)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(new
                {
                    token = GenerateJwt(user, roles)
                });
            }

            return BadRequest("Mot de passe inccorect.");
        }

        [HttpPost("Roles")]

        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Le rôle doit avoir un nom");
            }

            var newRole = new Role
            {
                Name = roleName
            };

            var roleResult = await _roleManager.CreateAsync(newRole);

            if (roleResult.Succeeded)
            {
                return Ok();
            }

            return Problem(roleResult.Errors.First().Description, null, 500);
        }

        [HttpPost("user/{userEmail}/role")]

        public async Task<IActionResult> AddUserToRole(string userEmail, [FromBody] string roleName)
        {

            var user = _userManager.Users.SingleOrDefault(u => u.Email == userEmail);

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Problem(result.Errors.First().Description, null, 500);
        }

        private string GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.Value.ExpirationInDays));
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Value.Issuer,
                audience: _jwtSettings.Value.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
