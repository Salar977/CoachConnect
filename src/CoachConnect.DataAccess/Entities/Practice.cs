using System.ComponentModel.DataAnnotations;

namespace CoachConnect.DataAccess.Entities;

public partial class Practice
{
    [Key]
    public int Id { get; set; }

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
