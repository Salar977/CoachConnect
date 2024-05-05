using CoachConnect.BusinessLayer.DTOs.Practices;
using FluentValidation;

namespace CoachConnect.BusinessLayer.Validators.PracticeValidators;

public class PracticeRequestValidator : AbstractValidator<PracticeRequest>
{
    public PracticeRequestValidator()
    {
        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Cannot be empty")
            .MinimumLength(2).WithMessage("Must be at least 2 characters long")
            .MaximumLength(50).WithMessage("Must not exceed 50 characters");

        RuleFor(x => x.PracticeDate)
            .NotEmpty().WithMessage("Cannot be empty")
            .GreaterThan(DateTime.Now).WithMessage("Date must be in the future")
            .LessThan(DateTime.Now.AddMonths(1)).WithMessage("Date must be less than 1 month in the future");
    }
}