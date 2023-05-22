using GameDay.API.DbContext;
using GameDay.API.Models;

namespace GameDay.API.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly Game_Day_DBContext _context;
        public MatchRepository(Game_Day_DBContext context)
        {
            _context = context;
        }
        public void CreateMatch(Match match)
        {
            _context.Match.Add(match);

            _context.SaveChanges();
        }

        public void DeleteMatch(int matchId)
        {
            Match? match = _context.Match.Find(matchId);

            _context.Match.Remove(match);
        }

        public Match? GetMatchById(int matchId)
        {
            return _context.Match.Find(matchId);
        }

        public IEnumerable<Match> GetMatches()
        {
            return _context.Match.ToList();
        }

        public IEnumerable<Match> GetMatchByTeamId(int teamId)
        {
            return _context.Match.Where(m => m.TeamId == teamId).ToList();
        }

        public void UpdateMatch(Match match)
        {
            _context.Update(match);

            _context.SaveChanges();
        }
    }
}
