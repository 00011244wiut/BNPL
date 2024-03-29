using Application.DTOs.Product;
using MediatR;

namespace Application.Features.Product.Queries.GetProductById;
// GetProductCommand record to get product by ID
public record GetProductCommand(Guid Id) : IRequest<ProductResponseDto>;