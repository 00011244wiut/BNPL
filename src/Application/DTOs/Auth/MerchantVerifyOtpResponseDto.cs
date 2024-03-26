namespace Application.DTOs.Auth;

public record MerchantVerifyOtpResponseDto(string AccessToken, string RefreshToken, MerchantResponseDto Merchant);