using Application.Contracts;
using FluentValidation;

namespace Application.Features.Product.Commands.CreateProduct;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        
    }
}