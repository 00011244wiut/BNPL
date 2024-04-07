using Application.DTOs.Product;
using MediatR;

namespace Application.Features.Product.Queries.GetAllProducts;

public record GetAllProductsCommand : IRequest<List<ProductResponseDto>>;