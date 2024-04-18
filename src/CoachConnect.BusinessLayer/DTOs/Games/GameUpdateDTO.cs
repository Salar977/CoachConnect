using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs.Games;
public record GameUpdateDTO(

    string Location,
    string OpponentName,
    DateTime GameTime
    );
