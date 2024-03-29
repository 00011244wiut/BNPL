using Application.DTOs.Auth;
using Application.DTOs.BankInfo;
using MediatR;

namespace Application.Features.Merchant.Commands.RegisterBankInfo;

// Command for registering bank information
public record RegisterBankInfoCommand(RegisterBankInfoDto RegisterBankInfoDto, Guid MerchantId) : IRequest<MerchantResponseDto>;