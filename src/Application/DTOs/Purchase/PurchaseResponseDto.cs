namespace Application.DTOs.Purchase;

// Data transfer object (DTO) representing a response for a purchase.
public record PurchaseResponseDto(Guid Id, string ProductName, DateTime CreatedTime);