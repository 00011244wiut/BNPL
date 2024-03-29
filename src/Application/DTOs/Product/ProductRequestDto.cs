namespace Application.DTOs.Product;

// Data transfer object (DTO) representing a request for product creation.
public record ProductRequestDto(
    string ProductName,
    decimal PriceAmount,
    string PreviewPhotoLink
);