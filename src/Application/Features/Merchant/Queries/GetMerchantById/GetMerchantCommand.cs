using Application.DTOs.Auth;
using MediatR;

namespace Application.Features.Merchant.Queries.GetMerchantById;
// GetMerchantCommand record to retrieve merchant by ID
public record GetMerchantCommand(Guid Id) : IRequest<MerchantResponseDto>;