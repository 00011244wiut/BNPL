namespace Application.DTOs.User;

// Data transfer object (DTO) representing card information submission.
public record SubmitCardDto(string CardNumber, string ExpiryDate, int CardType, string CVV);