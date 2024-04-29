using FluentValidation;

namespace Application.Features.Auth.Commands.VerifyOtp;

// Validator for the VerifyOtpCommand
public class VerifyOtpValidator : AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpValidator()
    {
        // Rule for PhoneNumber
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required")
            .Matches(@"^\+998[0-9]{9}$")
            .WithMessage("Phone Number must start with '+998' followed by exactly 9 digits.");
    }
}