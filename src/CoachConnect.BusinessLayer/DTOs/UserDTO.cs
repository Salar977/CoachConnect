namespace CoachConnect.BusinessLayer.DTOs;

public record UserDTO(
    int Id,
    string UserName,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email,
    DateTime Created,
    DateTime Updated);