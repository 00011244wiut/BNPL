using Application.Contracts;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Auth.Commands.MerchantSignUpSignIn;

public class MerchantSignUpSignInHandler : IRequestHandler<MerchantSignUpSignInCommand, MerchantStatus>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public MerchantSignUpSignInHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<MerchantStatus> Handle(MerchantSignUpSignInCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new MerchantSignUpSignInValidator().ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var merchant = await _unitOfWork.MerchantRepository.GetByPhoneNumberAsync(request.PhoneNumber);

        if (merchant != null) return merchant.MerchantStatus;
        // Add Phone Number to Simulation Table
        var simulation = new SimulationEntity()
        {
            PhoneNumber = request.PhoneNumber,
            SampleOTP = "1234"
        };
        await _unitOfWork.SimulationRepository.AddAsync(simulation);
            
        // Create a new User With Phone Number and User State as NotComplete
        merchant = new MerchantEntity()
        {
            MerchantStatus = MerchantStatus.NotComplete,
            PhoneNumber = request.PhoneNumber,
        };
            
        await _unitOfWork.MerchantRepository.AddAsync(merchant);

        // Return User State
        return merchant.MerchantStatus;

    }
}