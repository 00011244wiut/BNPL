namespace Application.DTOs.Product;

// Data transfer object (DTO) representing a request for updating a product.
public record ProductUpdateDto(
    string? ProductName,
    decimal? PriceAmount,
    string? PreviewPhotoLink
);