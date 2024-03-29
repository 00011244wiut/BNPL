using FluentValidation;

namespace Application.Features.Order.Commands.ConfirmOrder;
// ConfirmOrderValidator class to validate ConfirmOrderCommand
public class ConfirmOrderValidator : AbstractValidator<ConfirmOrderCommand>
{
    // Constructor for ConfirmOrderValidator
    public ConfirmOrderValidator()
    {
        // Validation rules for ConfirmOrderCommand can be added here if needed
    }
}