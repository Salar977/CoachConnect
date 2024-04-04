using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs;

public class UserDTO 
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }  
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public UserId Id { get; set; }
    public IEnumerable<PlayerDTO>? Players { get; set; }
}