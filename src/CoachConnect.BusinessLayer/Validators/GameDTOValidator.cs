using CoachConnect.BusinessLayer.DTOs;
using FluentValidation;
using System;

namespace CoachConnect.BusinessLayer.Validators
{
    public class GameDTOValidator : AbstractValidator<GameDTO>
    {
        public GameDTOValidator()
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
                .NotEmpty().WithMessage("GameTime is required.");
        }
    }
}
