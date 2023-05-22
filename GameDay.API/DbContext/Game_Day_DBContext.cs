using GameDay.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameDay.API.DbContext
{
    public class Game_Day_DBContext : IdentityDbContext<User, Role, Guid>
    {
        public Game_Day_DBContext(DbContextOptions<Game_Day_DBContext> options) : base(options)
        {

        }

        public DbSet<Match> Match { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Training> Training { get; set; }
    }
}
