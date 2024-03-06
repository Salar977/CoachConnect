namespace CoachConnect.BusinessLayer.DTOs;

public record CoachDTO(
    string FirstName,
    string LastName,
    string Phonenumber,
    // List<TeamDTO> Teams,
    string Email);