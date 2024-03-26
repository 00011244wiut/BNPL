using Application.DTOs.LegalData;
using MediatR;

namespace Application.Features.Merchant.Queries.GetMerchantByTaxPayerId;

public record GetMerchantByTaxPayerIdCommand(string Id) : IRequest<LegalDataResponseDto>;