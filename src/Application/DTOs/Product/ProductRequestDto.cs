namespace Application.DTOs.Product;

public record ProductRequestDto(
    string ProductName,
    decimal PriceAmount,
    string PreviewPhotoLink
);

// @ProductsEntity
// - BusinessId: Guid
// - ProductName: string
// - PriceAmount: decimal
// - PreviewPhotoLink: string
// - CreatedTime: DateTime