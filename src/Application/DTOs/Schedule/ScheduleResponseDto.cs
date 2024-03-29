namespace Application.DTOs.Schedule;

// Data transfer object (DTO) representing a response for a schedule.
public record ScheduleResponseDto(
    decimal Amount,
    DateTime PaymentDate
);