namespace CoachConnect.BusinessLayer.DTOs.Players;
public record PlayerRequest(string FirstName,
                            string LastName,
                            Guid UserId,
                            Guid TeamId);