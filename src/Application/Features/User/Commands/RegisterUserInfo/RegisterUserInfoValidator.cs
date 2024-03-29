using FluentValidation;

namespace Application.Features.User.Commands.RegisterUserInfo;
// RegisterUserInfoValidator class to validate RegisterUserInfoCommand
public class RegisterUserInfoValidator : AbstractValidator<RegisterUserInfoCommand>
{
    public RegisterUserInfoValidator()
    {
        
    }
}