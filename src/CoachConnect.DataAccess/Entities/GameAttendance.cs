using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachConnect.DataAccess.Entities;

public readonly record struct GameAttendanceId(Guid gameAttendanceId)
{
    public static GameAttendanceId NewId => new GameAttendanceId(Guid.NewGuid());
    public static GameAttendanceId Empty => new GameAttendanceId(Guid.Empty);

};

public class GameAttendance
{
    [Key]
    public GameAttendanceId Id { get; set; }

    [ForeignKey(nameof(GameId))]
    public GameId GameId { get; set; }

    [ForeignKey(nameof(PlayerId))]
    public PlayerId PlayerId { get; set; }

    [Required]
    public DateTime Created { get; init; }

    [Required]
    public DateTime Updated { get; set; }

    public virtual Game? Game { get; set; }

    public virtual Player? Player { get; set; }
}
