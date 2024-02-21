using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachConnect.DataAccess.Entities;

public class Team
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(CoachId))]
    public int CoachId { get; set; }

    public string TeamCity { get; set; } = string.Empty;

    public string TeamName { get; set; } = string.Empty;

    public DateTime Created { get; init; }

    public DateTime Updated { get; set; }

    public virtual Coach? Coach { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
