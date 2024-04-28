using FluentValidation; // Importing FluentValidation for command validation

namespace Application.Features.User.Commands.SubmitCard; // Namespace declaration for the SubmitCardValidator feature

// Validator for the SubmitCardCommand
public class SubmitCardValidator : AbstractValidator<SubmitCardCommand>
{
    public SubmitCardValidator()
    {
        // Rule for CardNumber
        RuleFor(x => x.SubmitCardDto.CardNumber)
            .NotEmpty().WithMessage("Card Number is required")
            .Matches("^[0-9]{16}$")
            .WithMessage("Card Number must contain 16 digits.");
        
        // Rule for ExpiryDate
        RuleFor(x => x.SubmitCardDto.ExpiryDate)
            .NotEmpty().WithMessage("Expiry Date is required")
            .Matches("^(0[1-9]|1[0-2])/[0-9]{2}$")
            .WithMessage("Expiry Date must be in the format MM/YY.");
        
        // Rule for CVV
        RuleFor(x => x.SubmitCardDto.CVV)
            .NotEmpty().WithMessage("CVV is required")
            .Matches("^[0-9]{3}$")
            .WithMessage("CVV must contain 3 digits.");
    }
}