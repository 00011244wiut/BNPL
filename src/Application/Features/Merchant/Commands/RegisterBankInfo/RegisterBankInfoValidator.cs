using FluentValidation;

namespace Application.Features.Merchant.Commands.RegisterBankInfo;

// Validator for the RegisterBankInfoCommand
public class RegisterBankInfoValidator : AbstractValidator<RegisterBankInfoCommand>
{
    public RegisterBankInfoValidator()
    {
        // You can add validation rules here if needed
    }
}