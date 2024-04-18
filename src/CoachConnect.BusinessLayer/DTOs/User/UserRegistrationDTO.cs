namespace CoachConnect.BusinessLayer.DTOs.Users;

public record UserRegistrationDTO(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Password,
    string Email
    );