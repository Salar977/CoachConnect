using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachConnect.DataAccess.Entities;


public readonly record struct PracticeAttendanceId(Guid practiceAttendanceId)
{
    public static PracticeAttendanceId NewId => new PracticeAttendanceId(Guid.NewGuid());
    public static PracticeAttendanceId Empty => new PracticeAttendanceId(Guid.Empty);

};

public class PracticeAttendance
{
    [Key]
    public PracticeAttendanceId Id { get; set; }

    [ForeignKey(nameof(PracticeId))]
    public PracticeId PracticeId { get; set; }

    [ForeignKey(nameof(PlayerId))]
    public PlayerId PlayerId { get; set; }

    [Required]
    public DateTime Created { get; init; }

    [Required]
    public DateTime Updated { get; set; }

    public virtual Player? Player { get; set; }

    public virtual Practice? Practice { get; set; }
}
