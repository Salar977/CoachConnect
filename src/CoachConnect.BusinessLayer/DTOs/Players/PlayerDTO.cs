using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.DTOs.Players;

public record PlayerDTO
(
    string FirstName,
    string LastName,

    DateTime Created,
    DateTime Updated,
    UserId UserId,
    TeamId TeamId,
    PlayerId Id);

/*
public class PlayerDTO
{
    public PlayerDTO(string firstName, string lastName, DateTime? created, DateTime updated, Guid userId, Guid teamId, Guid id)
    {
        FirstName = firstName;
        LastName = lastName;
        Created = created;
        Updated = updated;
        UserId = userId;
        TeamId = teamId;
        Id = id;
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? Created { get; set; }
    public DateTime Updated { get; set; }
    public Guid UserId { get; set; }
    public Guid TeamId { get; set; }
    public Guid Id { get; set; }




}
*/