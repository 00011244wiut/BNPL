using Domain.Constants;

namespace Application.DTOs.Card;

public record CardResponseDto(
    Guid Id,
    CardTypes CardType,
    string CardNumber
);