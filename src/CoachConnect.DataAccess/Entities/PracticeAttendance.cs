using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachConnect.DataAccess.Entities;

public partial class PracticeAttendance
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(PracticeId))]
    public int PracticeId { get; set; }

    [ForeignKey(nameof(PlayerId))]
    public int PlayerId { get; set; }

    [Required]
    public DateTime Created { get; init; }

    [Required]
    public DateTime Updated { get; set; }

    public virtual Player? Player { get; set; }

    public virtual Practice? Practice { get; set; }
}
