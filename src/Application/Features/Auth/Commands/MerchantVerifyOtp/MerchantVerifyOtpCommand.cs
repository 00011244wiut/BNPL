using Application.DTOs.Auth;
using MediatR;

namespace Application.Features.Auth.Commands.MerchantVerifyOtp;

public record MerchantVerifyOtpCommand(string PhoneNumber, string SampleOTP) : IRequest<MerchantVerifyOtpResponseDto>;