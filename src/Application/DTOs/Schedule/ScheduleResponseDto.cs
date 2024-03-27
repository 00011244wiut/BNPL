namespace Application.DTOs.Schedule;

public record ScheduleResponseDto(
    decimal Amount,
    DateTime PaymentDate
);