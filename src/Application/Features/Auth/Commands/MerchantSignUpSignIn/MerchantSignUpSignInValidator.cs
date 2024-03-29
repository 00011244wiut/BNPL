using FluentValidation;

namespace Application.Features.Auth.Commands.MerchantSignUpSignIn;

// Validator for the MerchantSignUpSignInCommand
public class MerchantSignUpSignInValidator : AbstractValidator<MerchantSignUpSignInCommand>
{
    public MerchantSignUpSignInValidator()
    {
        // You can add validation rules here if needed
    }
}