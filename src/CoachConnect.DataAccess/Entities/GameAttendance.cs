using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachConnect.DataAccess.Entities;

public partial class GameAttendance
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(GameId))]
    public int GameId { get; set; }

    [ForeignKey(nameof(PlayerId))]
    public int PlayerId { get; set; }

    [Required]
    public DateTime Created { get; init; }

    [Required]
    public DateTime Updated { get; set; }

    public virtual Game? Game { get; set; }

    public virtual Player? Player { get; set; }
}
