using FluentValidation;

namespace Application.Features.Auth.Commands.MerchantVerifyOtp;

// Validator for the MerchantVerifyOtpCommand
public class MerchantVerifyOtpValidator : AbstractValidator<MerchantVerifyOtpCommand>
{
    public MerchantVerifyOtpValidator()
    {
        // Rule for PhoneNumber
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required")
            .Matches(@"^\+998[0-9]{8}$")
            .WithMessage("Phone Number must start with '+998' followed by exactly 8 digits.");
    }
}