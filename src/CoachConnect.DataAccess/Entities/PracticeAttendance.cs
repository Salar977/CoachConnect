namespace CoachConnect.DataAccess.Entities;

public partial class PracticeAttendance
{
    public int Id { get; set; }

    public int PracticeId { get; set; }

    public int PlayerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Player Player { get; set; } = null!;

    public virtual Practice Practice { get; set; } = null!;
}
