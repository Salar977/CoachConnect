namespace CoachConnect.DataAccess.Entities;

public partial class Practice
{
    public int Id { get; set; }

    public string Location { get; set; } = null!;

    public DateTime PracticeDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<PracticeAttendance> PracticeAttendances { get; set; } = new List<PracticeAttendance>();
}
