namespace Application.DTOs.LegalData;

// Data transfer object (DTO) representing a response for legal data.
public record LegalDataResponseDto(
    string City,
    string BusinessType,
    string LegalName,
    string LegalAddress,
    string DirectorName
);