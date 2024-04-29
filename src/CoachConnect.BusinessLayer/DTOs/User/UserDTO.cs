using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs.Users;

public class UserDTO
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public UserId Id { get; init; }
    public IEnumerable<PlayerDTO>? Players { get; set; } // skulle hatt HATEOAS implementert ikke tid 
}
