using FluentValidation; // Importing FluentValidation for command validation

namespace Application.Features.User.Commands.SubmitCard; // Namespace declaration for the SubmitCardValidator feature

// Validator for the SubmitCardCommand
public class SubmitCardValidator : AbstractValidator<SubmitCardCommand>
{
    public SubmitCardValidator()
    {
        // Validation rules for SubmitCardCommand can be added here
    }
}