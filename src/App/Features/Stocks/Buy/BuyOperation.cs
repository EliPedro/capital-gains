using CapitalGain.Features.Stocks.Entities;
using CapitalGain.Features.Stocks.Factories;

namespace CapitalGain.Features.Stocks.Buy
{
    public class BuyOperation : IOperation
    {
        public decimal Execute(Stock stock, int quantity, decimal price)
        {
            stock.CalculateWeightedAverage(quantity, price);
            return stock.NoTaxToPay();
        }
    }
}