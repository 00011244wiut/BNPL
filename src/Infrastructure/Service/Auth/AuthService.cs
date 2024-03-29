// Importing necessary namespaces and contracts
using Application.Contracts;
using Application.Exceptions;
using Domain.Constants;
using Domain.Entities;

// Namespace for authentication service implementation
namespace Infrastructure.Service.Auth;

// Implementation of the authentication service contract
public class AuthService : IAuthService
{
    // Field to store the unit of work instance
    private readonly IUnitOfWork _unitOfWork;
    
    // Constructor to initialize the AuthService with a unit of work instance
    public AuthService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    // Method to verify OTP asynchronously for a user
    public async Task<UserEntity> VerifyOtpAsync(string phoneNumber, string sampleOTP)
    {
        // Retrieving simulation by phone number from repository
        var simulation = await _unitOfWork.SimulationRepository.GetSimulationByPhoneNumberAsync(phoneNumber);
        // Handling case where simulation is not found
        if (simulation == null)
        {
            throw new NotFoundException("Phone Number not found");
        }
        
        // Checking if sample OTP matches with the provided OTP
        if (simulation.SampleOTP != sampleOTP)
        {
            throw new BadRequestException("Invalid OTP");
        }
        
        // Retrieving user by phone number from repository
        var user = await _unitOfWork.UserRepository.GetByPhoneNumberAsync(phoneNumber);
        
        // If user is not in 'NotComplete' state, return the user
        if (user!.UserState != UserState.NotComplete) return user;
        
        // Updating user state and verification status
        user.UserState = UserState.PhoneNumberConfirmed;
        user.IsPhoneConfirmed = true;
        user.IsPhoneVerificationSuccess = true;
        await _unitOfWork.UserRepository.UpdateAsync(user);

        // Returning the updated user
        return user;
    }
    
    // Method to verify OTP asynchronously for a merchant
    public async Task<MerchantEntity> MerchantVerifyOtpAsync(string phoneNumber, string sampleOTP)
    {
        // Retrieving simulation by phone number from repository
        var simulation = await _unitOfWork.SimulationRepository.GetSimulationByPhoneNumberAsync(phoneNumber);
        // Handling case where simulation is not found
        if (simulation == null)
        {
            throw new NotFoundException("Phone Number not found");
        }
        
        // Checking if sample OTP matches with the provided OTP
        if (simulation.SampleOTP != sampleOTP)
        {
            throw new BadRequestException("Invalid OTP");
        }
        
        // Retrieving merchant by phone number from repository
        var merchant = await _unitOfWork.MerchantRepository.GetByPhoneNumberAsync(phoneNumber);
        
        // If merchant is not in 'NotComplete' state, return the merchant
        if (merchant!.MerchantStatus != MerchantStatus.NotComplete) return merchant;
        
        // Updating merchant status and verification success status
        merchant.MerchantStatus = MerchantStatus.PhoneNumberConfirmed;
        merchant.IsVerificationSuccess = true;
        
        // Updating merchant in repository
        await _unitOfWork.MerchantRepository.UpdateAsync(merchant);

        // Returning the updated merchant
        return merchant;
    }
}
