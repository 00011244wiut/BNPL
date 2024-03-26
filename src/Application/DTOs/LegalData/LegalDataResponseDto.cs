namespace Application.DTOs.LegalData;

public record LegalDataResponseDto(
    string City,
    string BusinessType,
    string LegalName,
    string LegalAddress,
    string DirectorName
);