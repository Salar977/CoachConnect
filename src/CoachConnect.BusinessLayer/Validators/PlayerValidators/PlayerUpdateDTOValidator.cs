using CoachConnect.BusinessLayer.DTOs.Players;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Validators.PlayerValidators;
public class PlayerUpdateDTOValidator : AbstractValidator<PlayerUpdate>
{
    public PlayerUpdateDTOValidator()
    {
        RuleFor(player => player.FirstName)
            .NotEmpty().WithMessage("FirstName can not be null")
            .MaximumLength(16).WithMessage("FirstName limit exceeded (max 16 characters)");
        RuleFor(player => player.LastName)
            .NotEmpty().WithMessage("LastName can not be null")
            .MaximumLength(16).WithMessage("LastName limit exceeded (max 16 characters)");
    }
}
