using CoachConnect.BusinessLayer.DTOs.Games;
using FluentValidation;
using System;

namespace CoachConnect.BusinessLayer.Validators
{
    public class GameDTOValidator : AbstractValidator<GameDTO>
    {
        public GameDTOValidator()
        {        

            RuleFor(game => game.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MinimumLength(3).WithMessage("Location must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Location must not exceed 50 characters.");

            RuleFor(game => game.GameTime)
                .NotEmpty().WithMessage("GameTime is required.");
        }
    }
}
