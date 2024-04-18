namespace CoachConnect.BusinessLayer.DTOs.Users;

public record UserCoachUpdateDTO(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email);
