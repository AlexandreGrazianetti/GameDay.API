using GameDay.API.Models;

namespace GameDay.API.Repositories
{
    public interface IMatchRepository
    {
        void CreateMatch(Match match);
        void DeleteMatch(int matchId);
        Match? GetMatchById(int matchId);
        IEnumerable<Match> GetMatches();
        void UpdateMatch(Match match);
        IEnumerable<Match> GetMatchByTeamId(int teamId);
    }
}
