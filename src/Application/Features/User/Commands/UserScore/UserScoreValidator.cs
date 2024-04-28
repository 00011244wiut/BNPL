using FluentValidation;

namespace Application.Features.User.Commands.UserScore;

public class UserScoreValidator : AbstractValidator<UserScoreCommand>
{
    public UserScoreValidator()
    {
        // Rule for income Enter Income value between 1000000 UZS and 25000000 UZS
        RuleFor(x => x.Income)
            .InclusiveBetween(1000000, 25000000)
            .WithMessage("Enter Income value between 1000000 UZS and 25000000 UZS");
    }
}