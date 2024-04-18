using FluentValidation;
using CoachConnect.BusinessLayer.DTOs.Games;

namespace CoachConnect.BusinessLayer.Validators
{
    public class GameAttendanceDTOValidator : AbstractValidator<GameAttendanceDTO>
    {
        public GameAttendanceDTOValidator()
        {
            RuleFor(GameAttendance => GameAttendance.GameId)
                .NotEmpty()
                .WithMessage("GameId is required.");

            RuleFor(GameAttendance => GameAttendance.PlayerId)
                .NotEmpty()
                .WithMessage("PlayerId is required.");
        }
    }
}
