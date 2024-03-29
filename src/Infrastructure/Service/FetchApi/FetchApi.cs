// Importing necessary namespaces and contracts
using Application.Contracts;


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
}