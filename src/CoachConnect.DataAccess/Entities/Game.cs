namespace CoachConnect.DataAccess.Entities;

public partial class Game
{
    public int Id { get; set; }

    public string Location { get; set; } = null!;

    public string OpponentName { get; set; } = null!;

    public DateTime GameTime { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<GameAttendance> GameAttendances { get; set; } = new List<GameAttendance>();
}
