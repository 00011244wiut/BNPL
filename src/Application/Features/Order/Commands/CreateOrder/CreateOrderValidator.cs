using FluentValidation;

namespace Application.Features.Order.Commands.CreateOrder;
// CreateOrderValidator class to validate CreateOrderCommand
public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    // Constructor for CreateOrderValidator
    public CreateOrderValidator()
    {
        // Validation rules for CreateOrderCommand can be added here if needed
    }
}