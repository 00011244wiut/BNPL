using Application.Contracts;
using FluentValidation;

namespace Application.Features.Product.Commands.CreateProduct;
// CreateProductValidator class to validate CreateProductCommand
public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    // Constructor for CreateProductValidator
    public CreateProductValidator()
    {
        // Validation rules for CreateProductCommand can be added here if needed
    }
}