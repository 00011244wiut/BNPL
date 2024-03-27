using FluentValidation;

namespace Application.Features.Order.Commands.ConfirmOrder;

public class ConfirmOrderValidator : AbstractValidator<ConfirmOrderCommand>
{
    public ConfirmOrderValidator()
    {
        
    }
}