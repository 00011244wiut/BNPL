using Application.DTOs.KYC;
using Microsoft.AspNetCore.Http;

namespace Application.Contracts;

// Interface for fetching API data and performing linear regression predictions
public interface IFetchApi
{
    // Asynchronously predicts using linear regression based on income.
    // Parameters:
    //   income: The income value for prediction.
    // Returns:
    //   A Task representing the asynchronous operation. The task result contains a decimal value representing the prediction result.
    Task<decimal> PredictLinerRegressionAsync(string income);
    Task<KycResponseDto> CallComparePhotosApi(IFormFile idPhoto, IFormFile userPhoto);
}