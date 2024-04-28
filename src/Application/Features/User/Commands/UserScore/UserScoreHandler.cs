using Application.Contracts;
using Application.DTOs.Auth;
using Application.DTOs.Prediction;
using Application.Exceptions;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.User.Commands.UserScore;

public class UserScoreHandler : IRequestHandler<UserScoreCommand, (UserResponseDto, decimal)>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFetchApi _fetchApi;
    
    public UserScoreHandler(IUnitOfWork unitOfWork, IMapper mapper, IFetchApi fetchApi)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fetchApi = fetchApi;
    }
    
    public async Task<(UserResponseDto, decimal)> Handle(UserScoreCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new UserScoreValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);

        if (user == null)
            throw new NotFoundException("User not found");
        
        if (user == null) throw new NotFoundException("User not found");
        var userEntity = _mapper.Map<UserEntity>(user);

        var purchaseLimit = new PurchaseLimitEntity()
        {
            UserId = userEntity.Id,
            ScoringDate = DateTime.UtcNow,
            MaxAmount = await _fetchApi.PredictLinerRegressionAsync(new PredictionRequestDto(request.Income, request.Age, request.Gender, request.MaritalStatus)),
            PurchaseLimitType = "CREDIT"
        };
        
        await _unitOfWork.PurchaseLimitRepository.AddAsync(purchaseLimit);
        
        userEntity.PurchaseLimitId = purchaseLimit.Id;
        
        if (userEntity.UserState == UserState.VerificationCompleted &&
            userEntity is { FirstName: not null, CardId: not null })
            userEntity.UserState = UserState.CompleteProfile;
        
        await _unitOfWork.UserRepository.UpdateAsync(userEntity);
        return (_mapper.Map<UserResponseDto>(userEntity), purchaseLimit.MaxAmount);
    }
}