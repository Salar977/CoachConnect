namespace CoachConnect.BusinessLayer.DTOs.Teams;
public record TeamResponse
(
    string TeamCity,
    string TeamName,
    DateTime Created,
    DateTime Updated,
    Guid CoachId,
    Guid Id);