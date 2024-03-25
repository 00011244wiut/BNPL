using Domain.Entities;

namespace Application.Contracts;

public interface IAuthService
{
    Task<UserEntity> VerifyOtpAsync(string phoneNumber, string sampleOtp);
}