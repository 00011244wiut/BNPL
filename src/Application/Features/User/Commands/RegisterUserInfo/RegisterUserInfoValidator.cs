using FluentValidation;

namespace Application.Features.User.Commands.RegisterUserInfo;

public class RegisterUserInfoValidator : AbstractValidator<RegisterUserInfoCommand>
{
    public RegisterUserInfoValidator()
    {
        
    }
}