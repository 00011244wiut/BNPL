namespace Application.DTOs.LegalData;

public record LegalDataRequestDto(
        string City,
        string BusinessType,
        string LegalName,
        string LegalAddress,
        string DirectorName
);