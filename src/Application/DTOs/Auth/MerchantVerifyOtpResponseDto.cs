namespace Application.DTOs.Auth;

// Data transfer object (DTO) representing a response for verifying OTP for a merchant.
public record MerchantVerifyOtpResponseDto(string AccessToken, string RefreshToken, MerchantResponseDto Merchant);