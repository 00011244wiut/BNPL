using FluentValidation;

namespace Application.Features.Auth.Commands.SignUpSignIn;

// Validator for the SignUpSignInCommand
public class SignUpSignInValidator : AbstractValidator<SignUpSignInCommand>
{
    public SignUpSignInValidator()
    {
        // Rule for PhoneNumber
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required")
            .Matches(@"^\+998[0-9]{8}$")
            .WithMessage("Phone Number must start with '+998' followed by exactly 8 digits.");
    }
}