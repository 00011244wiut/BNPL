// Importing necessary namespaces and contracts
using System.Net.Http.Headers;
using System.Text;
using Application.Contracts;
using Application.DTOs.KYC;
using Application.DTOs.Prediction;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


// Namespace for FetchApi service implementation
namespace Infrastructure.Service.FetchApi;

// Implementation of the FetchApi service contract
public class FetchApi : IFetchApi
{
    // Method to predict linear regression asynchronously based on income
    public async Task<decimal> PredictLinerRegressionAsync(PredictionRequestDto predictionRequestDto)
    {
        // Third party API URL
        const string url = "https://credit-limit-prediction.onrender.com/predict/xgboost_regression";
        var httpClient = new HttpClient();

        var jsonData = new
        {
            predictionRequestDto.Income,
            predictionRequestDto.Age,
            Gender = predictionRequestDto.Gender.ToString(),
            Married = predictionRequestDto.Married.ToString(),
            Cards = 3,
            Education = 11,
        };
        
        // Serializing the JSON data
        var json = JsonConvert.SerializeObject(jsonData);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        
        // Sending a POST request to the third party API
        var response = await httpClient.PostAsync(url, data);
        var responseJson = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode || responseJson == null) throw new BadRequestException($"Third party(Prediction API) failed to respond. \n{responseJson?? "No response"}");
        var responseObject = JsonConvert.DeserializeObject<PredictionResponseDto>(responseJson);

        return responseObject?.prediction ?? throw new BadRequestException("Third party API failed to respond.");
    }
    

    public async Task<KycResponseDto> CallComparePhotosApi(IFormFile idPhoto, IFormFile userPhoto)
    {
        // Third party API URL
        const string url = "http://20.205.137.66:5000/compare-photos";
        var httpClient = new HttpClient();
        
        // Creating a new MultipartFormDataContent
        using var formData = new MultipartFormDataContent();
        
        // Adding ID photo and user photo to the form data
        formData.Add(new StreamContent(idPhoto.OpenReadStream()), "id_photo", idPhoto.FileName);
        formData.Add(new StreamContent(userPhoto.OpenReadStream()), "user_photo", userPhoto.FileName);

        // Setting the content type
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        // Sending a POST request to the third party API
        var response = await httpClient.PostAsync(url, formData);
        if (!response.IsSuccessStatusCode) throw new BadRequestException("Third party(KYC API) failed to respond. ");
        var responseJson = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<KycResponseDto>(responseJson);
        
        // Returning the response object
        return responseObject ?? throw new BadRequestException("Third party API failed to respond.");
    }
}