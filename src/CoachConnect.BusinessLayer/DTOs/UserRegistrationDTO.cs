namespace CoachConnect.BusinessLayer.DTOs;

public record UserRegistrationDTO(   
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Password,
    string Email
    );