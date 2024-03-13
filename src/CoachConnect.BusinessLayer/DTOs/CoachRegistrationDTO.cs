namespace CoachConnect.BusinessLayer.DTOs;

public record CoachRegistrationDTO(
    string FirstName,
    string LastName,
    string PhoneNumber,
   // string Team, // velge fra liste eksisterende Teams. Deretter må få godkjent av eier av Teamet??
    string Password,
    string Email
    );