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

    [Required]
    [MinLength(2), MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MinLength(2), MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    public int TotalGames { get; set; } 
    public int TotalPractices { get; set; } 

    [ForeignKey(nameof(UserId))]
    public UserId UserId { get; set; }

    [ForeignKey(nameof(TeamId))]
    public TeamId TeamId { get; set; }

    [Required]
    public DateTime Created { get; set; }

    [Required]
    public DateTime Updated { get; set; }

    public virtual ICollection<GameAttendance> GameAttendances { get; set; } = new List<GameAttendance>();

    public virtual ICollection<PracticeAttendance> PracticeAttendances { get; set; } = new List<PracticeAttendance>();

    public virtual Team? Team { get; set; }

    public virtual User? User { get; set; }
}
