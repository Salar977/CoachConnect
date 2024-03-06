namespace CoachConnect.BusinessLayer.DTOs;

public record CoachDTO(
    string FirstName,
    string LastName,
    // List<TeamDTO> Teams,
    string PhoneNumber,    
    string Email);