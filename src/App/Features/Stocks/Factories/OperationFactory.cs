using CapitalGain.Core;
using CapitalGain.Features.Stocks.Buy;
using CapitalGain.Features.Stocks.Sell;

namespace CapitalGain.Features.Stocks.Factories
{
    public static class OperationFactory
    {
        public static IOperation CreateOperation(string type)
        {
            return type.ToLower() switch
            {
                OperationType.Buy => new BuyOperation(),
                OperationType.Sell => new SellOperation(),
                _ => throw new ArgumentException("Invalid operation type.")
            };
        }
    }
}