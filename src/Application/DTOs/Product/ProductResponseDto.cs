namespace Application.DTOs.Product;

// Data transfer object (DTO) representing a response for a product.
public record ProductResponseDto(
    Guid Id,
    string ProductName,
    decimal PriceAmount,
    string PreviewPhotoLink
);