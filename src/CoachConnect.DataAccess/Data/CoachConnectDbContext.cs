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

        modelBuilder.Entity<Coach>()
            .Property(x => x.Id)
            .HasConversion(
                id => id.coachId,
                value => new CoachId(value)
            );

        modelBuilder.Entity<Game>()
            .Property(x => x.Id)
            .HasConversion(
                id => id.gameId,
                value => new GameId(value)
            );

        modelBuilder.Entity<GameAttendance>()
            .Property(x => x.Id)
            .HasConversion(
                id => id.gameAttendanceId,
                value => new GameAttendanceId(value)
            );

        modelBuilder.Entity<Practice>()
            .Property(x => x.Id)
            .HasConversion(
                id => id.practiceId,
                value => new PracticeId(value)
            );

        modelBuilder.Entity<PracticeAttendance>()
            .Property(x => x.Id)
            .HasConversion(
                id => id.practiceAttendanceId,
                value => new PracticeAttendanceId(value)
            );

        modelBuilder.Entity<Team>()
            .Property(x => x.Id)
            .HasConversion(
                id => id.teamId,
                value => new TeamId(value)
            );


        // Herfra og nedover: Configure the mapping for Player.UserId // trenger denne og pga vi har Foreignkey Userid i Player.cs (Ketils comment ikke slett comment inntil videre)
        modelBuilder.Entity<Player>()
            .Property(p => p.UserId)
            .HasConversion(
                v => v.userId,  // Convert UserId to underlying type
                v => new UserId(v)
            );  // Convert underlying type to UserId

        modelBuilder.Entity<GameAttendance>()
            .Property(x => x.GameId)
            .HasConversion(
                id => id.gameId,
                value => new GameId(value)
            );

        modelBuilder.Entity<GameAttendance>()
           .Property(x => x.PlayerId)
           .HasConversion(
               id => id.playerId,
               value => new PlayerId(value)
           );

        modelBuilder.Entity<Player>()
           .Property(x => x.TeamId)
           .HasConversion(
               id => id.teamId,
               value => new TeamId(value)
           );

        modelBuilder.Entity<PracticeAttendance>()
         .Property(x => x.PlayerId)
         .HasConversion(
             id => id.playerId,
             value => new PlayerId(value)
           );

        modelBuilder.Entity<PracticeAttendance>()
          .Property(x => x.PracticeId)
          .HasConversion(
              id => id.practiceId,
              value => new PracticeId(value)
           );

        modelBuilder.Entity<Team>()
         .Property(x => x.CoachId)
         .HasConversion(
             id => id.coachId,
             value => new CoachId(value)
          );
    }
}
