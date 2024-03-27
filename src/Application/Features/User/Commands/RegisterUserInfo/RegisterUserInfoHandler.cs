using Application.Contracts;
using Application.DTOs.Auth;
using AutoMapper;
using Domain.Constants;
using FluentValidation;
using MediatR;

namespace Application.Features.User.Commands.RegisterUserInfo;

public class RegisterUserInfoHandler : IRequestHandler<RegisterUserInfoCommand, UserResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public RegisterUserInfoHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<UserResponseDto> Handle(RegisterUserInfoCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new RegisterUserInfoValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        var userEntity = _mapper.Map<Domain.Entities.UserEntity>(user);

        _mapper.Map(request.RegisterUserInfoDto, userEntity);
        
        if (userEntity.UserState == UserState.VerificationCompleted &&
            userEntity is { PurchaseLimitId: not null, CardId: not null })
            userEntity.UserState = UserState.CompleteProfile;
        
        await _unitOfWork.UserRepository.UpdateAsync(userEntity);
        return _mapper.Map<UserResponseDto>(userEntity);
    }
}