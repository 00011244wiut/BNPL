// Importing necessary namespaces and contracts
using System.Net.Http.Headers;
using Application.Contracts;
using Application.DTOs.KYC;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


// Namespace for FetchApi service implementation
namespace Infrastructure.Service.FetchApi;

// Implementation of the FetchApi service contract
public class FetchApi : IFetchApi
{
    // Method to predict linear regression asynchronously based on income
    public Task<decimal> PredictLinerRegressionAsync(string income)
    {
        // Throwing NotImplementedException as the method is not implemented yet
        throw new NotImplementedException();
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
        var responseJson = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<KycResponseDto>(responseJson);
        
        // Returning the response object
        return responseObject ?? throw new BadRequestException("Third party API failed to respond.");
    }
}