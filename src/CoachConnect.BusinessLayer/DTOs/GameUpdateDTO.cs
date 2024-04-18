using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs;
public record GameUpdateDTO(

    string Location,
    string OpponentName,
    DateTime GameTime
    );
