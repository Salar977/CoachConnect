using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachConnect.DataAccess.Entities;

public partial class Player
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(UserId))]
    public int UserId { get; set; }

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
