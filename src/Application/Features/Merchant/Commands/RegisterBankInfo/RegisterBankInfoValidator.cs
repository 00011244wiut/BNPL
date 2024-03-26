using FluentValidation;

namespace Application.Features.Merchant.Commands.RegisterBankInfo;

public class RegisterBankInfoValidator : AbstractValidator<RegisterBankInfoCommand>
{
    public RegisterBankInfoValidator()
    {
        
    }
}