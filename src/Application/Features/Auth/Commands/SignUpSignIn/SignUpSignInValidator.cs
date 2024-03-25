using FluentValidation;

namespace Application.Features.Auth.Commands.SignUpSignIn;

public class SignUpSignInValidator : AbstractValidator<SignUpSignInCommand>
{
    public SignUpSignInValidator()
    {
        
    }
}