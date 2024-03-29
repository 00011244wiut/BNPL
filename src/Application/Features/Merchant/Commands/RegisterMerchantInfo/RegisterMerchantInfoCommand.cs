using Application.DTOs.LegalData;
using Application.DTOs.Merchant;
using MediatR;

namespace Application.Features.Merchant.Commands.RegisterMerchantInfo;

// Command for registering merchant information
public record RegisterMerchantInfoCommand(RegisterMerchantInfoDto RegisterMerchantInfoDto, Guid MerchantId) : IRequest<LegalDataResponseDto>;