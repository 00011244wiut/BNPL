using Domain.Entities;

namespace Application.Contracts;

// Interface for authentication service providing methods to verify OTP for users and merchants
public interface IAuthService
{
    // Asynchronously verifies OTP for a user.
    // Parameters:
    //   phoneNumber: The phone number of the user.
    //   sampleOtp: The OTP code entered by the user.
    // Returns:
    //   A Task representing the asynchronous operation. The task result contains a UserEntity if OTP verification is successful; otherwise, null.
    Task<UserEntity> VerifyOtpAsync(string phoneNumber, string sampleOtp);

    // Asynchronously verifies OTP for a merchant.
    // Parameters:
    //   phoneNumber: The phone number of the merchant.
    //   sampleOtp: The OTP code entered by the merchant.
    // Returns:
    //   A Task representing the asynchronous operation. The task result contains a MerchantEntity if OTP verification is successful; otherwise, null.
    Task<MerchantEntity> MerchantVerifyOtpAsync(string phoneNumber, string sampleOtp);
}