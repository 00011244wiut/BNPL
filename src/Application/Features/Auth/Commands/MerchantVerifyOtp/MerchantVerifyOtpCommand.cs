using Application.DTOs.Auth;
using MediatR;

namespace Application.Features.Auth.Commands.MerchantVerifyOtp;

// Command for verifying OTP for a merchant
public record MerchantVerifyOtpCommand(string PhoneNumber, string SampleOTP) : IRequest<MerchantVerifyOtpResponseDto>;