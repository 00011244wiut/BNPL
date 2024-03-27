namespace Application.DTOs.Purchase;

public record PurchaseResponseDto(Guid Id, string ProductName, DateTime CreatedTime);