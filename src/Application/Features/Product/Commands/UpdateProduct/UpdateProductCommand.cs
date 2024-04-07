using Application.DTOs.Product;
using MediatR;

namespace Application.Features.Product.Commands.UpdateProduct;
// UpdateProductCommand record to update a product
public record UpdateProductCommand(Guid ProductId, ProductUpdateDto ProductDto, Guid MerchantId) : IRequest<Unit>;