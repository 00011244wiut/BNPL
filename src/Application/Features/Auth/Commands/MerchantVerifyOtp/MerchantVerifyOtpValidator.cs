using FluentValidation;

namespace Application.Features.Auth.Commands.MerchantVerifyOtp;

// Validator for the MerchantVerifyOtpCommand
public class MerchantVerifyOtpValidator : AbstractValidator<MerchantVerifyOtpCommand>
{
    public MerchantVerifyOtpValidator()
    {
        // You can add validation rules here if needed
    }
}