using FluentValidation;

namespace Application.Features.Merchant.Commands.RegisterMerchantInfo;
// RegisterMerchantNameValidator class to validate RegisterMerchantNameCommand
public class RegisterMerchantInfoValidator : AbstractValidator<RegisterMerchantInfoCommand>
{
    // Constructor for RegisterMerchantNameValidator
    public RegisterMerchantInfoValidator()
    {
        // Rule for Company Name
        RuleFor(x => x.RegisterMerchantInfoDto.CompanyName)
            .NotEmpty().WithMessage("Company Name is required")
            .Matches("^[A-Z][a-zA-Z]*$")
            .WithMessage("Company Name must start with a capital letter and only contain English alphabet letters.");
        
        // Rule for City
        RuleFor(x => x.RegisterMerchantInfoDto.City)
            .NotEmpty().WithMessage("City is required")
            .Matches("^[A-Z][a-zA-Z]*$")
            .WithMessage("City must start with a capital letter and only contain English alphabet letters.");
        
        // Rule for TaxPayerId
        RuleFor(x => x.RegisterMerchantInfoDto.TaxPayerId)
            .NotEmpty().WithMessage("Tax Payer Id is required")
            .Matches("^[0-9]{9}$")
            .WithMessage("Tax Payer Id must contain 9 digits.");
    }
}