using FluentValidation;

namespace Application.Features.Auth.Commands.MerchantVerifyOtp;

public class MerchantVerifyOtpValidator : AbstractValidator<MerchantVerifyOtpCommand>
{
    public MerchantVerifyOtpValidator()
    {
        
    }
}