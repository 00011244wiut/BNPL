using FluentValidation;

namespace Application.Features.Merchant.Commands.RegisterMerchantInfo;
// RegisterMerchantInfoValidator class to validate RegisterMerchantInfoCommand
public class RegisterMerchantInfoValidator : AbstractValidator<RegisterMerchantInfoCommand>
{
    // Constructor for RegisterMerchantInfoValidator
    public RegisterMerchantInfoValidator()
    {
        // Validation rules for RegisterMerchantInfoCommand can be added here if needed
    }
}