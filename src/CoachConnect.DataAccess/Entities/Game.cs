using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachConnect.DataAccess.Entities;

public class Game
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Location { get; set; } = string.Empty;

    [Required]
    public string OpponentName { get; set; } = string.Empty;

    [Required]
    public DateTime GameTime { get; set; }

    [Required]
    public DateTime Created { get; init; }

    [Required]
    public DateTime Updated { get; set; }

    public virtual ICollection<GameAttendance> GameAttendances { get; set; } = new List<GameAttendance>();
}
