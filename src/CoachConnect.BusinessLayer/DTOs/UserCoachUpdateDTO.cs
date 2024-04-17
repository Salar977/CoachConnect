namespace CoachConnect.BusinessLayer.DTOs;

public record UserCoachUpdateDTO(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email);
  