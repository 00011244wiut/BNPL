using FluentValidation;

namespace Application.Features.Merchant.Commands.RegisterMerchantInfo;
// RegisterMerchantNameValidator class to validate RegisterMerchantNameCommand
public class RegisterMerchantInfoValidator : AbstractValidator<RegisterMerchantInfoCommand>
{
    // Constructor for RegisterMerchantNameValidator
    public RegisterMerchantInfoValidator()
    {
        // Validation rules for RegisterMerchantNameCommand can be added here if needed
    }
}