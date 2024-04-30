using CoachConnect.BusinessLayer.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Validators.GameValidators;
public class GameRegistrationDTOValidator : AbstractValidator<GameRegistrationDTO>
{
    public GameRegistrationDTOValidator()
    {
        RuleFor(game => game.OpponentName)
            .NotEmpty().WithMessage("OpponentName is required.")
            .MinimumLength(3).WithMessage("OpponentName must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("OpponentName must not exceed 50 characters.");

        RuleFor(game => game.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MinimumLength(3).WithMessage("Location must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Location must not exceed 50 characters.");

        RuleFor(game => game.GameTime)
            .NotEmpty().WithMessage("GameTime is required.")
            .Must(BeInFuture).WithMessage("GameTime must be in the future.");
    }

    private bool BeInFuture(DateTime gameTime)
    {
        return gameTime > DateTime.Now;
    }
}
