using Application.DTOs.Product;
using MediatR;

namespace Application.Features.Product.Commands.CreateProduct;

public record CreateProductCommand (ProductRequestDto ProductRequestDto, Guid MerchantId) : IRequest<Guid>;