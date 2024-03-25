using FluentValidation;

namespace Application.Features.Auth.Commands.VerifyOtp;

public class VerifyOtpValidator : AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpValidator()
    {
        
    }
}