using AutoMapper;
using GameDay.API.Dto.Match;
using GameDay.API.Models;
using GameDay.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace GameDay.API.Controllers
{
    [ApiController]
    [Route("api/match")]
    public class MatchController : Controller
    {
        //C'est comme si on écrivait httpps://localhost/teachers

        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public MatchController(IMatchRepository matchRepository, UserManager<User> userManager)
        {
            _matchRepository = matchRepository;
            _userManager = userManager;
        }

        [Authorize(Roles = "Coach")]
        [HttpPost]
        public async Task<ActionResult> CreateMatch(CreateMatchDto matchDto)
        {
            Match match = new Match
            {
                TimeDebMatch = matchDto.TimeDebMatch,
                TimeFinMatch = matchDto.TimeFinMatch,
                WinMatch = matchDto.WinMatch,
                LoseMatch = matchDto.LoseMatch,
                EgalMatch = matchDto.EgalMatch
                
            };

            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            User connectedUser = await _userManager.FindByIdAsync(userId);

            match.TeamId = connectedUser.TeamId;

            _matchRepository.CreateMatch(match);

            return Ok();
        }

        [Authorize]
        [HttpGet("{matchId}")]
        public async Task<ActionResult<Match>> GetMatchById(int matchId)
        {
            Match match = _matchRepository.GetMatchById(matchId);

            if (match == null)
            {
                return NotFound();
            }

            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            User connectedUser = await _userManager.FindByIdAsync(userId);

            if (match.TeamId != connectedUser.TeamId)
            {
                return Forbid("Not your team.");
            }

            return Ok(_matchRepository.GetMatchById(matchId));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatch()
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            User connectedUser = await _userManager.FindByIdAsync(userId);

            return Ok(_matchRepository.GetMatchByTeamId(connectedUser.TeamId));
        }

        [Authorize(Roles = "Coach")]
        [HttpPut("{matchId}")]
        public async Task<ActionResult> UpdateMatch(UpdateMatchDto matchDto, int matchId)
        {
            Match? match = _matchRepository.GetMatchById(matchId);

            if (match == null)
            {
                return NotFound();
            }

            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            User connectedUser = await _userManager.FindByIdAsync(userId);

            if (match.TeamId != connectedUser.TeamId)
            {
                return Forbid("Not your team.");
            }

            Match matchToUpdate = _mapper.Map(matchDto, match);

            _matchRepository.UpdateMatch(matchToUpdate);

            return Ok();
        }

        [HttpDelete("{matchId}")]
        public async Task<ActionResult> DeleteMatch(int matchId)
        {
            Match? match = _matchRepository.GetMatchById(matchId);

            if (match == null)
            {
                return NotFound();
            }

            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            User connectedUser = await _userManager.FindByIdAsync(userId);

            if (match.TeamId != connectedUser.TeamId)
            {
                return Forbid("Not your team.");
            }

            _matchRepository.DeleteMatch(matchId);

            return NoContent();
        }

    }
}
