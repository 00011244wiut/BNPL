using Application.DTOs.Sales;
using MediatR;

namespace Application.Features.Sales.Query.GetSalesByMerchantId;

public record GetSalesByMerchantIdCommand(Guid MerchantId) : IRequest<List<SalesResponseDto>?>;