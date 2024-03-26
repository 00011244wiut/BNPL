using Application.Contracts;
using Application.DTOs.Auth;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Auth.Commands.VerifyOtp;

public class VerifyOtpHandler : IRequestHandler<VerifyOtpCommand, VerifyOtpResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    
    public VerifyOtpHandler(IUnitOfWork unitOfWork, IAuthService authService, ITokenService tokenService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _tokenService = tokenService;
        _mapper = mapper;
    }
    
    public async Task<VerifyOtpResponseDto> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new VerifyOtpValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var user = await _authService.VerifyOtpAsync(request.PhoneNumber, request.SampleOTP);
    
        var accessToken = _tokenService.GenerateAccessToken(user);
        var (tokenId, refreshToken) = _tokenService.GenerateRefreshToken();
        
        await _unitOfWork.TokenRepository.AddAsync(new TokenEntity()
        {
            Id = tokenId,
            RefreshToken = refreshToken,
            UserId = user.Id,
            User = user,
        });

        return new VerifyOtpResponseDto(
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            User: _mapper.Map<UserResponseDto>(user)
        );
    }
}