using Application.DTOs.Product;
using MediatR;

namespace Application.Features.Product.Commands.DeleteProduct;
// DeleteProductCommand record to update a product
public record DeleteProductCommand(Guid ProductId, Guid MerchantId) : IRequest<Unit>;