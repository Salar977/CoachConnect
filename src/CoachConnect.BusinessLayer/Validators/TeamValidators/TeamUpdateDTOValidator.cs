using CoachConnect.BusinessLayer.DTOs.Teams;
using FluentValidation;

namespace CoachConnect.BusinessLayer.Validators.TeamValidators;
public class TeamUpdateDTOValidator : AbstractValidator<TeamUpdate>
{
    public TeamUpdateDTOValidator()
    {
        RuleFor(team => team.TeamCity)
        .NotEmpty().WithMessage("TeamCity can not be null")
        .MinimumLength(3).WithMessage("TeamCity must contain atleast 3 characters")
        .MaximumLength(16).WithMessage("TeamCity limit exceeded (max 16 characters)");
        RuleFor(team => team.TeamName)
            .NotEmpty().WithMessage("LastName can not be null")
            .MinimumLength(3).WithMessage("TeamName must contain atleast 3 characters")
            .MaximumLength(32).WithMessage("TeamName limit exceeded (max 32 characters)");
    }
}
