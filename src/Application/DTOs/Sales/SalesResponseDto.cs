namespace Application.DTOs.Sales;

public record SalesResponseDto(
    Guid Id,
    Guid ProductId,
    string ProductName,
    decimal SaleAmount,
    DateTime CreatedTime
);