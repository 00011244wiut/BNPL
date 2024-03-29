namespace Application.DTOs.LegalData;

// Data transfer object (DTO) representing a request for legal data.
public record LegalDataRequestDto(
    string City,
    string BusinessType,
    string LegalName,
    string LegalAddress,
    string DirectorName
);