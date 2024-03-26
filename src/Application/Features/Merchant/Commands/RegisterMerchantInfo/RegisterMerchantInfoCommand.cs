using Application.DTOs.Auth;
using Application.DTOs.LegalData;
using Application.DTOs.Merchant;
using MediatR;

namespace Application.Features.Merchant.Commands.RegisterMerchantInfo;

public record RegisterMerchantInfoCommand(RegisterMerchantInfoDto  RegisterMerchantInfoDto, Guid MerchantId) : IRequest<LegalDataResponseDto>;