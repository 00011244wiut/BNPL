using Application.Contracts;
using Application.DTOs.Auth;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.User.Commands.UserScore;

public class UserScoreHandler : IRequestHandler<UserScoreCommand, UserResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public UserScoreHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<UserResponseDto> Handle(UserScoreCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new UserScoreValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        var userEntity = _mapper.Map<UserEntity>(user);
        
        
        
        await _unitOfWork.UserRepository.UpdateAsync(userEntity);
        return _mapper.Map<UserResponseDto>(userEntity);
    }
}