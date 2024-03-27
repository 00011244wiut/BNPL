namespace Application.DTOs.Payment;

public record PaymentResponseDto(
    Guid Id,
    string ProductName,
    DateTime CreatedTime,
    Guid MerchantId, 
    (
        decimal Amount,
        DateTime CreatedTime
    ) Schedule
);