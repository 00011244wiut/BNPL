using Application.DTOs.Auth;
using Application.DTOs.BankInfo;
using MediatR;

namespace Application.Features.Merchant.Commands.RegisterBankInfo;

public record RegisterBankInfoCommand(RegisterBankInfoDto RegisterBankInfoDto, Guid MerchantId) : IRequest<MerchantResponseDto>;