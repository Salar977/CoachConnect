using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs;

public class UserDTO
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserId Id { get; init; }
    public IEnumerable<PlayerDTO>? Players { get; set; }
}

//public record UserDTO(

//    string FirstName,
//    string LastName,
//    IEnumerable<Player> Players,
//    string PhoneNumber,
//    string Email,
//    UserId Id);