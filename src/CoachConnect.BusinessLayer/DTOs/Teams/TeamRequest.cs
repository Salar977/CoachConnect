namespace CoachConnect.BusinessLayer.DTOs.Teams;
public record TeamRequest(string TeamCity,
                          string TeamName,
                          Guid CoachId);
