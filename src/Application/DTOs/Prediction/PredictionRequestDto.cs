using Domain.Constants;

namespace Application.DTOs.Prediction;

public record PredictionRequestDto(int Income, int Age, Gender Gender, MaritalStatus Married);