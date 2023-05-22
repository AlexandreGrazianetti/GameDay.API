using GameDay.API.DbContext;
using GameDay.API.Models;

namespace GameDay.API.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly Game_Day_DBContext _context;
        public TeamRepository(Game_Day_DBContext context)
        {
            _context = context;
        }

        public IEnumerable<Team> GetTeams()
        {
            return _context.Team.ToList();
        }
        public void DeleteTeamById(int teamId)
        {
            Team? team = _context.Team.Find(teamId);
            _context.Team.Remove(team);
        }

        public Team? GetTeamById(int teamId)
        {
            return _context.Team.Find(teamId);
        }


        public void UpdateTeam(Team team)
        {
            throw new NotImplementedException();
        }

        public void CreateTeam(Team team)
        {
            _context.Team.Add(team);
            _context.SaveChanges(); 
        }

    }
}
