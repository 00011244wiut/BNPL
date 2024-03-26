using Application.Contracts;
using Application.Exceptions;
using Domain.Constants;
using Domain.Entities;

namespace Infrastructure.Service.Auth;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public AuthService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UserEntity> VerifyOtpAsync(string phoneNumber, string sampleOTP)
    {
        var simulation = await _unitOfWork.SimulationRepository.GetSimulationByPhoneNumberAsync(phoneNumber);
        if (simulation == null)
        {
            throw new NotFoundException("Phone Number not found");
        }
        
        if (simulation.SampleOTP != sampleOTP)
        {
            throw new BadRequestException("Invalid OTP");
        }
        
        var user = await _unitOfWork.UserRepository.GetByPhoneNumberAsync(phoneNumber);
        
        if (user!.UserState != UserState.NotComplete) return user;
        
        user.UserState = UserState.PhoneNumberConfirmed;
        user.IsPhoneConfirmed = true;
        user.IsPhoneVerificationSuccess = true;
        await _unitOfWork.UserRepository.UpdateAsync(user);

        return user;
    }
    
    public async Task<MerchantEntity> MerchantVerifyOtpAsync(string phoneNumber, string sampleOTP)
    {
        var simulation = await _unitOfWork.SimulationRepository.GetSimulationByPhoneNumberAsync(phoneNumber);
        if (simulation == null)
        {
            throw new NotFoundException("Phone Number not found");
        }
        
        if (simulation.SampleOTP != sampleOTP)
        {
            throw new BadRequestException("Invalid OTP");
        }
        
        var merchant = await _unitOfWork.MerchantRepository.GetByPhoneNumberAsync(phoneNumber);
        
        if (merchant!.MerchantStatus != MerchantStatus.NotComplete) return merchant;
        merchant.MerchantStatus = MerchantStatus.PhoneNumberConfirmed;
        merchant.IsVerificationSuccess = true;
        
        await _unitOfWork.MerchantRepository.UpdateAsync(merchant);

        return merchant;
    }
}