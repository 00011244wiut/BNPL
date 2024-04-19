using Application.DTOs.Product;
using MediatR;

namespace Application.Features.Product.Queries.GetAllProducts;

public record GetAllProductsCommand(Guid MerchantId) : IRequest<List<ProductResponseDto>>;