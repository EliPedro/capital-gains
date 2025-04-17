using CapitalGain.Features.Stocks.Entities;
using CapitalGain.Features.Stocks.Factories;

namespace CapitalGain.Features.Stocks.Sell
{
    public class SellOperation : IOperation
    {
        public decimal Execute(Stock stock, int quantity, decimal price)
        {
            return stock.CalculateTax(quantity, price);
        }
    }
}