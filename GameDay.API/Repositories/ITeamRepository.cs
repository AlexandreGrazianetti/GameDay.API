using GameDay.API.Models;

namespace GameDay.API.Repositories
{
    public interface ITeamRepository
    {
        void CreateTeam(Team team);
        void DeleteTeamById(int teamId);
        IEnumerable<Team> GetTeams();
        Team? GetTeamById(int teamid);
        void UpdateTeam (Team team);
    }
}
