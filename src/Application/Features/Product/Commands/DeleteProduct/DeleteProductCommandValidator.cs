using FluentValidation;

namespace Application.Features.Product.Commands.DeleteProduct;
// DeleteProductCommandValidator class to validate DeleteProductCommand
public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    // Constructor for DeleteProductCommandValidator
    public DeleteProductCommandValidator()
    {
        // Validation rules for DeleteProductCommand can be added here if needed
    }
}