namespace Application.DTOs.KYC;

public record KycResponseDto(bool Match, double Similarity_percentage);
