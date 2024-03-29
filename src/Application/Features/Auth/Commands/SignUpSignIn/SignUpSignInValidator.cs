using FluentValidation;

namespace Application.Features.Auth.Commands.SignUpSignIn;

// Validator for the SignUpSignInCommand
public class SignUpSignInValidator : AbstractValidator<SignUpSignInCommand>
{
    public SignUpSignInValidator()
    {
        // You can add validation rules here if needed
    }
}