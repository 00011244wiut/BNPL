using Domain.Constants;

namespace Application.DTOs.Card;

// Data transfer object (DTO) representing a response for a card.
public record CardResponseDto(
    Guid Id,
    CardTypes CardType,
    string CardNumber
);