using CapitalGain.Features.Stocks.Entities;

namespace CapitalGain.Features.Stocks.Factories
{
    public interface IOperation
    {
        decimal Execute(Stock stock, int quantity, decimal price);
    }
}