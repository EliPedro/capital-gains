namespace CapitalGain.Features.Entities.Stocks.Services
{
    public interface IStockService
    {
        public Task ExecuteAsync(string filePath);
    }
}