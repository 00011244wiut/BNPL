namespace Application.DTOs.Sales;

// Data transfer object (DTO) representing a response for a sale.
public record SalesResponseDto(
    Guid Id,
    Guid ProductId,
    string ProductName,
    decimal SaleAmount,
    DateTime CreatedTime
);