using Application.DTOs.Product;
using MediatR;

namespace Application.Features.Product.Commands.UpdateProduct;

public record UpdateProductCommand(Guid ProductId, ProductUpdateDto ProductDto) : IRequest<Unit>;