using FluentValidation;

namespace Application.Features.Merchant.Commands.RegisterMerchantInfo;

public class RegisterMerchantInfoValidator : AbstractValidator<RegisterMerchantInfoCommand>
{
    public RegisterMerchantInfoValidator()
    {
        
    }
}