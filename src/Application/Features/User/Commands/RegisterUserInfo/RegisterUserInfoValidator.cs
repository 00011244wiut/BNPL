using FluentValidation;

namespace Application.Features.User.Commands.RegisterUserInfo;
// RegisterUserInfoValidator class to validate RegisterUserInfoCommand
public class RegisterUserInfoValidator : AbstractValidator<RegisterUserInfoCommand>
{
    public RegisterUserInfoValidator()
    {
        // Rule for FirstName
        RuleFor(x => x.RegisterUserInfoDto.FirstName)
            .NotEmpty().WithMessage("First Name is required")
            .Matches("^[A-Z][a-zA-Z]*$")
            .WithMessage("First Name must start with a capital letter and only contain English alphabet letters.");
        
        // Rule for LastName
        RuleFor(x => x.RegisterUserInfoDto.LastName)
            .NotEmpty().WithMessage("Last Name is required")
            .Matches("^[A-Z][a-zA-Z]*$")
            .WithMessage("Last Name must start with a capital letter and only contain English alphabet letters.");
    }
}