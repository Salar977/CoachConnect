using CoachConnect.BusinessLayer.DTOs.PracticeAttendanceDtos;
using FluentValidation;

namespace CoachConnect.BusinessLayer.Validators.PracticeAttendanceValidators;

public class PracticeAttendanceRequestValidator : AbstractValidator<PracticeAttendanceRequest>
{
    public PracticeAttendanceRequestValidator()
    {
        RuleFor(attendance => attendance.PracticeId)
                .NotEmpty()
                .WithMessage("PracticeId is required.");

        RuleFor(attendance => attendance.PlayerId)
            .NotEmpty()
            .WithMessage("PlayerId is required.");
    }
}