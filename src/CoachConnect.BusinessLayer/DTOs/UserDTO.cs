using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs;

public record UserDTO(   
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email);