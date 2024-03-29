using Application.DTOs.Auth;
using MediatR;

namespace Application.Features.Auth.Commands.VerifyOtp;

// Command for verifying OTP
public record VerifyOtpCommand(string PhoneNumber, string SampleOTP) : IRequest<VerifyOtpResponseDto>;