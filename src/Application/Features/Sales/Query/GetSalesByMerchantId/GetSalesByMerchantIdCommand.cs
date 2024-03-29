using Application.DTOs.Sales;
using MediatR;

namespace Application.Features.Sales.Query.GetSalesByMerchantId;
// GetSalesByMerchantIdCommand record to get sales by merchant ID
public record GetSalesByMerchantIdCommand(Guid MerchantId) : IRequest<List<SalesResponseDto>?>;