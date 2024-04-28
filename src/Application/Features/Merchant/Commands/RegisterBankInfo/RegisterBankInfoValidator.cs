using FluentValidation;

namespace Application.Features.Merchant.Commands.RegisterBankInfo;

// Validator for the RegisterBankInfoCommand
public class RegisterBankInfoValidator : AbstractValidator<RegisterBankInfoCommand>
{
    public RegisterBankInfoValidator()
    {
        // Rule for MFO
        RuleFor(x => x.RegisterBankInfoDto.MFO)
            .NotEmpty().WithMessage("MFO is required")
            .Matches("^[0-9]{5}$")
            .WithMessage("MFO must contain 5 digits.");
        
        // Rule for BankAccountNumber
        RuleFor(x => x.RegisterBankInfoDto.BankAccountNumber)
            .NotEmpty().WithMessage("Bank Account Number is required")
            .Matches("^[0-9]{21}$")
            .WithMessage("Bank Account Number must contain 21 digits.");
    }
}