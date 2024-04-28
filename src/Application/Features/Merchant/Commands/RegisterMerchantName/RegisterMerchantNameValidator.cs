using FluentValidation;

namespace Application.Features.Merchant.Commands.RegisterMerchantName;
// RegisterMerchantNameValidator class to validate RegisterMerchantNameCommand
public class RegisterMerchantNameValidator : AbstractValidator<RegisterMerchantNameCommand>
{
    // Constructor for RegisterMerchantNameValidator
    public RegisterMerchantNameValidator()
    {
        // Rule for FirstName
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First Name is required")
            .Matches("^[A-Z][a-zA-Z]*$")
            .WithMessage("First Name must start with a capital letter and only contain English alphabet letters.");
        
        // Rule for LastName
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last Name is required")
            .Matches("^[A-Z][a-zA-Z]*$")
            .WithMessage("Last Name must start with a capital letter and only contain English alphabet letters.");
    }
}