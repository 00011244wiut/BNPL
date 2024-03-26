namespace Application.Contracts;

public interface IFetchApi
{
    Task<decimal> PredictLinerRegressionAsync(string income);
}