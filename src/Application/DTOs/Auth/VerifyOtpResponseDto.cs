namespace Application.DTOs.Auth;

// Data transfer object (DTO) representing a response for verifying OTP for a user.
public record VerifyOtpResponseDto(string AccessToken, string RefreshToken, UserResponseDto User);