namespace Application.DTOs.User;

public record SubmitCardDto(string CardNumber, string ExpiryDate, int CardType, string CVV);