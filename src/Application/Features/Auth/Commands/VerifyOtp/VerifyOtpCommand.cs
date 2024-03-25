using Application.DTOs.Auth;
using MediatR;

namespace Application.Features.Auth.Commands.VerifyOtp;

public record VerifyOtpCommand(string PhoneNumber, string SampleOTP) : IRequest<VerifyOtpResponseDto>;