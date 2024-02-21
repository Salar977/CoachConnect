using CoachConnect.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoachConnect.DataAccess.Data;

public class CoachConnectDbContext : DbContext
{
    public CoachConnectDbContext(DbContextOptions<CoachConnectDbContext> options) : base(options)
    {

    }
    public DbSet<Coach> Coaches { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<GameAttendance> Game_attendences { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Practice> Practices { get; set; }
    public DbSet<PracticeAttendance> Practice_attendences { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<User> Users { get; set; }
}