namespace CoachConnect.DataAccess.Entities;

public partial class Team
{
    public int Id { get; set; }

    public int CoachId { get; set; }

    public string TeamCity { get; set; } = null!;

    public string TeamName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Coach Coach { get; set; } = null!;

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
