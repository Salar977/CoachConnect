using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs;

public record UserDTO(
    //UserId Id,
    //string UserName,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email,
    DateTime Created,
    DateTime Updated);