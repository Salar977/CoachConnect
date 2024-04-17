namespace CoachConnect.BusinessLayer.DTOs;

public record UserRegistrationDTO(   // legge til player med en gang også? nå må user først registrere seg og så registrere player på navnet sitt
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Password,
    string Email
    );