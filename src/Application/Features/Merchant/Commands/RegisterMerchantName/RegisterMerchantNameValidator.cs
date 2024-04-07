using FluentValidation;

namespace Application.Features.Merchant.Commands.RegisterMerchantName;
// RegisterMerchantNameValidator class to validate RegisterMerchantNameCommand
public class RegisterMerchantNameValidator : AbstractValidator<RegisterMerchantNameCommand>
{
    // Constructor for RegisterMerchantNameValidator
    public RegisterMerchantNameValidator()
    {
        // Validation rules for RegisterMerchantNameCommand can be added here if needed
    }
}