using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachConnect.DataAccess.Entities;

public readonly record struct PlayerId(Guid playerId)
{
    public static PlayerId NewId => new PlayerId(Guid.NewGuid());
    public static PlayerId Empty => new PlayerId(Guid.Empty);

};
public class Player
{
    [Key]
    public PlayerId Id { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserId UserId { get; set; }

    [ForeignKey(nameof(TeamId))]
    public int TeamId { get; set; }

    [Required]
    public DateTime Created { get; init; }

    [Required]
    public DateTime Updated { get; set; }

    public virtual ICollection<GameAttendance> GameAttendances { get; set; } = new List<GameAttendance>();

    public virtual ICollection<PracticeAttendance> PracticeAttendances { get; set; } = new List<PracticeAttendance>();

    public virtual Team? Team { get; set; }

    public virtual User? User { get; set; }
}
