namespace CoachConnect.DataAccess.Entities;

public partial class Player
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TeamId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<GameAttendance> GameAttendances { get; set; } = new List<GameAttendance>();

    public virtual ICollection<PracticeAttendance> PracticeAttendances { get; set; } = new List<PracticeAttendance>();

    public virtual Team Team { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
