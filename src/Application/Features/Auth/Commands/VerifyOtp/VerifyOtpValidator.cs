using FluentValidation;

namespace Application.Features.Auth.Commands.VerifyOtp;

// Validator for the VerifyOtpCommand
public class VerifyOtpValidator : AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpValidator()
    {
        // You can add validation rules here if needed
    }
}