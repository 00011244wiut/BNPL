using Application.DTOs.Schedule;

namespace Application.DTOs.Payment;

// Data transfer object (DTO) representing a response for a payment.
public record PaymentResponseDto(
    Guid Id,
    string ProductName,
    DateTime CreatedTime,
    Guid MerchantId, 
    List<ScheduleResponseDto> Schedules
    );