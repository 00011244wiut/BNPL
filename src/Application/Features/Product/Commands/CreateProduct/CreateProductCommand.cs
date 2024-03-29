using Application.DTOs.Product;
using MediatR;

namespace Application.Features.Product.Commands.CreateProduct;
// CreateProductCommand record to create a product
public record CreateProductCommand(ProductRequestDto ProductRequestDto, Guid MerchantId) : IRequest<Guid>;