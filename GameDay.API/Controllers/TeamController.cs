using GameDay.API.Models;
using GameDay.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameDay.API.Controllers
{
    [Route("api/teams")]
    [Authorize]
    [ApiController]
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;
        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Team>> GetTeams()
        {
            return Ok(_teamRepository.GetTeams());
        }
    }
}
