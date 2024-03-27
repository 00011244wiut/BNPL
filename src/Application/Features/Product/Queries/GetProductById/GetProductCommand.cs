using Application.DTOs.Product;
using MediatR;

namespace Application.Features.Product.Queries.GetProductById;

public record GetProductCommand(Guid Id) : IRequest<ProductResponseDto>;