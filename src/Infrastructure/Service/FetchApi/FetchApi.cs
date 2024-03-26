using Application.Contracts;

namespace Infrastructure.Service.FetchApi;

public class FetchApi : IFetchApi
{
    public Task<decimal> PredictLinerRegressionAsync(string income)
    {
        throw new NotImplementedException();
    }
}