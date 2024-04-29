using Application.Contracts;
using Application.Helpers;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Auth.Commands.MerchantSignUpSignIn;

// Handler for the MerchantSignUpSignInCommand
public class MerchantSignUpSignInHandler : IRequestHandler<MerchantSignUpSignInCommand, MerchantStatus>
{
    private readonly IUnitOfWork _unitOfWork;
    
    // Constructor
    public MerchantSignUpSignInHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    // Handles the command asynchronously
    public async Task<MerchantStatus> Handle(MerchantSignUpSignInCommand request, CancellationToken cancellationToken)
    {
        // Validate the command
        var validationResult = await new MerchantSignUpSignInValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        // Check if merchant exists
        var merchant = await _unitOfWork.MerchantRepository.GetByPhoneNumberAsync(request.PhoneNumber);

        // If merchant exists, return its status
        if (merchant != null) 
            return merchant.MerchantStatus;

        // If merchant doesn't exist, add phone number to Simulation table
        var simulation = new SimulationEntity()
        {
            PhoneNumber = request.PhoneNumber,
            SampleOTP = OtpHelper.GenerateRandomString(4)
        };
        await _unitOfWork.SimulationRepository.AddAsync(simulation);
            
        // Create a new merchant with phone number and status as NotComplete
        merchant = new MerchantEntity()
        {
            MerchantStatus = MerchantStatus.NotComplete,
            PhoneNumber = request.PhoneNumber,
        };
            
        await _unitOfWork.MerchantRepository.AddAsync(merchant);

        // Return merchant status
        return merchant.MerchantStatus;
    }
}
