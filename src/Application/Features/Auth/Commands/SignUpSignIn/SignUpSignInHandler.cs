using System.Text;
using Application.Contracts;
using Application.Helpers;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Auth.Commands.SignUpSignIn;

// Handler for the SignUpSignInCommand
public class SignUpSignInHandler : IRequestHandler<SignUpSignInCommand, UserState>
{
    private readonly IUnitOfWork _unitOfWork;
    
    // Constructor
    public SignUpSignInHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    // Handles the command asynchronously
    public async Task<UserState> Handle(SignUpSignInCommand request, CancellationToken cancellationToken)
    {
        // Validate the command
        var validationResult = await new SignUpSignInValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        // Check if user exists
        var user = await _unitOfWork.UserRepository.GetByPhoneNumberAsync(request.PhoneNumber);

        // If user exists, return its state
        if (user != null)
            return user.UserState;

        // If user doesn't exist, add phone number to Simulation table
        var simulation = new SimulationEntity()
        {
            PhoneNumber = request.PhoneNumber,
            SampleOTP = OtpHelper.GenerateRandomString(4)
        };
        await _unitOfWork.SimulationRepository.AddAsync(simulation);
            
        // Create a new user with phone number and state as NotComplete
        user = new UserEntity()
        {
            PhoneNumber = request.PhoneNumber,
            UserState = UserState.NotComplete
        };
            
        await _unitOfWork.UserRepository.AddAsync(user);

        // Return user state
        return user.UserState;        
    }
}