using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachConnect.DataAccess.Entities;


public readonly record struct TeamId(Guid teamId)
{
    public static TeamId NewId => new TeamId(Guid.NewGuid());
    public static TeamId Empty => new TeamId(Guid.Empty);

};


public class Team
{
    [Key]
    public TeamId Id { get; set; }

    [ForeignKey(nameof(CoachId))]
    public CoachId CoachId { get; set; }

    public string TeamCity { get; set; } = string.Empty;

    public string TeamName { get; set; } = string.Empty;
    public DateTime TeamTime { get; set; }

    public DateTime Created { get; init; }

    public DateTime Updated { get; set; }

    public virtual Coach? Coach { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
