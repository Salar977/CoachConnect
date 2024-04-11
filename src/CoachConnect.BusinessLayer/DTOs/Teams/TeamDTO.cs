using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.DTOs.Teams;

public record TeamDTO
(
    string TeamCity,
    string TeamName,
    DateTime Created,
    DateTime Updated,
    CoachId CoachId,
    TeamId Id);
/*
public class TeamDTO
{
    public TeamDTO(string teamCity, string teamName, DateTime? created, DateTime updated, Guid coachId, Guid id)
    {
        TeamCity = teamCity;
        TeamName = teamName;
        Created = created;
        Updated = updated;
        CoachId = coachId;
        Id = id; 
    }
    public string TeamCity { get; set; }
    public string TeamName { get; set; }
    public DateTime? Created { get; set; }
    public DateTime Updated { get; set; }
    public Guid CoachId { get; set; }
    public Guid Id { get; set; }




}

public class ArrangementRegisterDTO
{
    public ArrangementRegisterDTO(int id, int memberId, int arrangementId, DateTime? registrationDateTime, bool isConfirmed)
    {
        Id = id;
        MemberId = memberId;
        ArrangementId = arrangementId;
        RegistrationDateTime = registrationDateTime;
        IsConfirmed = isConfirmed;
    }

    public int Id { get; set; }
    public int MemberId { get; set; }
    public int ArrangementId { get; set; }
    public DateTime? RegistrationDateTime { get; set; }
    public bool IsConfirmed { get; set; }
}
 */