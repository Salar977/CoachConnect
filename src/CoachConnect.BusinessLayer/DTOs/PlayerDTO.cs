

using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs;

public class PlayerDTO
{

    public PlayerDTO(PlayerId id, UserId userId, TeamId teamId, string firstName, string lastName, DateTime? created, DateTime? update)
    {
        Id = id;
        UserId = userId;
        TeamId = teamId;
        FirstName = firstName;
        LastName = lastName;
        Created = created;
        Updated = update;

    }
    public PlayerId Id { get; set; }
    public UserId UserId { get; set; }
    public TeamId TeamId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? Created { get; set;}
    public DateTime? Updated { get; set;}
    public virtual Team? Team { get; set; }

    public virtual User? User { get; set; }
}





/*
public record PlayerDTO
(
    string FirstName,
    string LastName,
    DateTime Created,
    DateTime Updated,
    UserId UserId,
    TeamId TeamId,
    PlayerId Id);
*/