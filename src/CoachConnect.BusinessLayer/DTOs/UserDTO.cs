﻿using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs;

public record UserDTO(   
    
    string FirstName,
    string LastName,
   //List<PlayerDTO> Players,
    string PhoneNumber,
    string Email,
    UserId Id);