namespace Application.DTOs.Auth;

public record VerifyOtpResponseDto(string AccessToken, string RefreshToken, UserResponseDto User);