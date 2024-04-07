using Application.DTOs.Auth;
using MediatR;

namespace Application.Features.Merchant.Commands.RegisterMerchantName;

// Command for registering merchant information
public record RegisterMerchantNameCommand(string FirstName, string LastName, Guid MerchantId) : IRequest<MerchantResponseDto>;