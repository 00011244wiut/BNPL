using Application.Contracts;
using Application.Exceptions;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Service.Auth;

public class AuthService : IAuthService
{
    private readonly ProjectDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    
    public AuthService(ProjectDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
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
}