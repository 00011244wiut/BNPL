using Application.Contracts;
using Application.DTOs.Auth;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.User.Commands.SubmitCard;

public class SubmitCardHandler : IRequestHandler<SubmitCardCommand, UserResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public SubmitCardHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<UserResponseDto> Handle(SubmitCardCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new SubmitCardValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        var userEntity = _mapper.Map<UserEntity>(user);
        
        var card = _mapper.Map<CardsEntity>(request.SubmitCardDto);
        card = await _unitOfWork.CardsRepository.AddAsync(card);
        
        userEntity.CardId = card.Id;
        
        if (userEntity.UserState == UserState.VerificationCompleted &&
            userEntity is { FirstName: not null, PurchaseLimitId: not null })
            userEntity.UserState = UserState.CompleteProfile;
        
        await _unitOfWork.UserRepository.UpdateAsync(userEntity);
        return _mapper.Map<UserResponseDto>(userEntity);
    }
}