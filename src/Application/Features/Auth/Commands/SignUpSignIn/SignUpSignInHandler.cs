using Application.Contracts;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Auth.Commands.SignUpSignIn;

public class SignUpSignInHandler : IRequestHandler<SignUpSignInCommand, UserState>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public SignUpSignInHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UserState> Handle(SignUpSignInCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new SignUpSignInValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var user = await _unitOfWork.UserRepository.GetByPhoneNumberAsync(request.PhoneNumber);

        if (user == null)
        {
            // Add Phone Number to Simulation Table
            var simulation = new SimulationEntity()
            {
                PhoneNumber = request.PhoneNumber,
                SampleOTP = "1234"
            };
            await _unitOfWork.SimulationRepository.AddAsync(simulation);
            
            // Create a new User With Phone Number and User State as NotComplete
            user = new UserEntity()
            {
                PhoneNumber = request.PhoneNumber,
                UserState = UserState.NotComplete
            };
            
            await _unitOfWork.UserRepository.AddAsync(user);
            
        }
        
        // Return User State
        return user.UserState;
        
    }
}