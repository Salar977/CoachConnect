using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachConnect.DataAccess.Entities;

public readonly record struct GameId(Guid gameId)
{
    public static GameId NewId => new GameId(Guid.NewGuid());
    public static GameId Empty => new GameId(Guid.Empty);
};


public class Game
{
    [Key]
    public GameId Id { get; set; }

    [Required]
    public string Location { get; set; } = string.Empty;

    [Required]
    public string HomeTeam { get; set; } = string.Empty;

    [Required]
    public string AwayTeam { get; set; } = string.Empty;

    [Required]
    public DateTime GameTime { get; set; }

    [Required]
    public DateTime Created { get; init; }

    [Required]
    public DateTime Updated { get; set; }

    public virtual ICollection<GameAttendance> GameAttendances { get; set; } = new List<GameAttendance>();
}
