using Application.Contracts;
using Application.DTOs.Auth;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Auth.Commands.MerchantVerifyOtp;

// Handler for the MerchantVerifyOtpCommand
public class MerchantVerifyOtpHandler : IRequestHandler<MerchantVerifyOtpCommand, MerchantVerifyOtpResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    
    // Constructor
    public MerchantVerifyOtpHandler(IUnitOfWork unitOfWork, IAuthService authService, ITokenService tokenService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _tokenService = tokenService;
        _mapper = mapper;
    }
    
    // Handles the command asynchronously
    public async Task<MerchantVerifyOtpResponseDto> Handle(MerchantVerifyOtpCommand request, CancellationToken cancellationToken)
    {
        // Validate the command
        var validationResult = await new MerchantVerifyOtpValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Verify OTP for the merchant
        var merchant = await _authService.MerchantVerifyOtpAsync(request.PhoneNumber, request.SampleOTP);
    
        // Generate access token and refresh token
        var accessToken = _tokenService.GenerateMerchantAccessToken(merchant);
        var (tokenId, refreshToken) = _tokenService.GenerateRefreshToken();
        
        // Save the refresh token to the database
        await _unitOfWork.TokenRepository.AddAsync(new TokenEntity()
        {
            Id = tokenId,
            RefreshToken = refreshToken,
            MerchantId = merchant.Id,
            Merchant = merchant,
        });

        // Return the response DTO
        return new MerchantVerifyOtpResponseDto(
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            Merchant: _mapper.Map<MerchantResponseDto>(merchant)
        );
    }
}
