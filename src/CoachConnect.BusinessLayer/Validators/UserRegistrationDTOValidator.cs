using CoachConnect.BusinessLayer.DTOs;
using FluentValidation;

namespace CoachConnect.BusinessLayer.Validators;

public class UserRegistrationDTOValidator : AbstractValidator<UserRegistrationDTO>
{
    public UserRegistrationDTOValidator() // må endres etterhvert dersom vi oppdaterer DTOet husk
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName can not be null")
            .MaximumLength(16).WithMessage("FirstName limit exceeded (max 16 characters)");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName can not be null")
            .MaximumLength(16).WithMessage("LastName limit exceeded (max 16 characters)");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phonenumber can not be null")
            .Length(8).WithMessage("Phonenumber must be 8 digits");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password must be included")
            .MinimumLength(8).WithMessage("Password must contain at least 8 characters")
            .MaximumLength(16).WithMessage("Password limit exceeded (max 16 characters)")
            .Matches(@"[0-9]+").WithMessage("Password must contain at least 1 number")
            .Matches(@"[A-Z]+").WithMessage("Password must contain at least 1 uppercase letter")
            .Matches(@"[a-z]+").WithMessage("Password must contain at least 1 lowercase letter")
            .Matches(@"[!?*#_-]+").WithMessage("Password must contain at least 1 special character ('! ? * # _ -')");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email must be included")
            .EmailAddress().WithMessage("Email must be valid");
    }
}