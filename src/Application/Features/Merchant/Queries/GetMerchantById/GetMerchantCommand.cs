using Application.DTOs.Auth;
using MediatR;

namespace Application.Features.Merchant.Queries.GetMerchantById;

public record GetMerchantCommand(Guid Id) : IRequest<MerchantResponseDto>;