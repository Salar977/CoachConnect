using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs;
public record GameAttendanceDTO(

    //string PlayerName,  prøver å få playername på gameattendance DTO
    GameAttendanceId GameAttendanceId,
    GameId GameId,    
    PlayerId PlayerId);

