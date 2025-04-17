using CapitalGain.Features.Stocks.Entities;
using CapitalGain.Features.Stocks.Validators;

namespace CapitalGain.UnitTests.Features.Entities
{
    public class StockTests
    {
        [Fact(DisplayName = "Calculate Weighted Average")]
        public void CalculateWeightedAverage()
        {
            var stock = new Stock("ROXO34");
            stock.CalculateWeightedAverage(100, 50.00m);
            stock.CalculateWeightedAverage(50, 60.00m);

            Assert.Equal(150, stock.Quantity);
            Assert.Equal(53.33m, stock.WeightedAverage, 2);
        }

        [Fact(DisplayName = "Calculate Tax")]
        public void CalculateTax()
        {
            var stock = new Stock("ROXO34");
            stock.CalculateWeightedAverage(100, 50.00m);
            stock.CalculateWeightedAverage(50, 60.00m);

            decimal tax = stock.CalculateTax(30, 70.00m);

            Assert.Equal(0, tax);
            Assert.Equal(120, stock.Quantity);
        }

        [Fact(DisplayName = "Quantity of shares to sell is greater than the available quantity")]
        public void SellMoreThanAvailable()
        {
            var stock = new Stock("ROXO34");
            stock.CalculateWeightedAverage(100, 50.00m);

            var exception = Assert.Throws<StockException>(() => stock.CalculateTax(150, 70.00m));
            Assert.Equal("The quantity of shares to sell is greater than the available quantity.", exception.Message);
        }

        [Fact(DisplayName = "Sell Stock at a loss.")]
        public void SellAtLoss()
        {
            var stock = new Stock("ROXO34");
            stock.CalculateWeightedAverage(100, 50.00m);

            decimal tax = stock.CalculateTax(30, 30.00m);

            Assert.True(stock.AccumulatedLoss > 0);
        }
    }
}