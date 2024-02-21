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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .Property(x => x.Id)
            .HasConversion(
                id => id.userId,
                value => new UserId(value)
            );

        modelBuilder.Entity<Player>()
            .Property(x => x.Id)
            .HasConversion(
                id => id.playerId,
                value => new PlayerId(value)
            );

        // Configure the mapping for Player.UserId // trenger denne og pga vi har Foreignkey Userid i Player.cs
        modelBuilder.Entity<Player>()
            .Property(p => p.UserId)
            .HasConversion(
                v => v.userId,  // Convert UserId to underlying type
                v => new UserId(v));  // Convert underlying type to UserId
    }

}
