using System.ComponentModel.DataAnnotations;

namespace CoachConnect.DataAccess.Entities;

public readonly record struct PracticeId(Guid practiceId)
{
    public static PracticeId NewId => new PracticeId(Guid.NewGuid());
    public static PracticeId Empty => new PracticeId(Guid.Empty);

};

public class Practice
{
    [Key]
    public PracticeId Id { get; set; }

    [Required]
    public string Location { get; set; } = string.Empty;

    [Required]
    public DateTime PracticeDate { get; set; }

    [Required]
    public DateTime Created { get; init; }

    [Required]
    public DateTime Updated { get; set; }

    public virtual ICollection<PracticeAttendance> PracticeAttendances { get; set; } = new List<PracticeAttendance>();
}
