using Application.Contracts;
using Application.DTOs.Auth;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Auth.Commands.VerifyOtp;

// Handler for the VerifyOtpCommand
public class VerifyOtpHandler : IRequestHandler<VerifyOtpCommand, VerifyOtpResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    
    // Constructor
    public VerifyOtpHandler(IUnitOfWork unitOfWork, IAuthService authService, ITokenService tokenService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _tokenService = tokenService;
        _mapper = mapper;
    }
    
    // Handles the command asynchronously
    public async Task<VerifyOtpResponseDto> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        // Validate the command
        var validationResult = await new VerifyOtpValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Verify OTP for the user
        var user = await _authService.VerifyOtpAsync(request.PhoneNumber, request.SampleOTP);
    
        // Generate access token and refresh token
        var accessToken = _tokenService.GenerateAccessToken(user);
        var (tokenId, refreshToken) = _tokenService.GenerateRefreshToken();
        
        // Save the refresh token to the database
        await _unitOfWork.TokenRepository.AddAsync(new TokenEntity()
        {
            Id = tokenId,
            RefreshToken = refreshToken,
            UserId = user.Id,
            User = user,
        });

        // Return the response DTO
        return new VerifyOtpResponseDto(
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            User: _mapper.Map<UserResponseDto>(user)
        );
    }
}
