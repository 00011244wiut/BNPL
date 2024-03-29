using Application.DTOs.LegalData;
using MediatR;

namespace Application.Features.Merchant.Queries.GetMerchantByTaxPayerId;
// GetMerchantByTaxPayerIdCommand record to retrieve merchant by tax payer ID
public record GetMerchantByTaxPayerIdCommand(string Id) : IRequest<LegalDataResponseDto>;