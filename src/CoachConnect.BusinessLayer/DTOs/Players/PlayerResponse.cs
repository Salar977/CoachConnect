namespace CoachConnect.BusinessLayer.DTOs.Players;
public record PlayerResponse
(
    string FirstName,
    string LastName,
    int TotalGames,
    int TotalPractices,
    DateTime Created,
    DateTime Updated,
    Guid UserId,
    Guid TeamId,
    Guid Id);