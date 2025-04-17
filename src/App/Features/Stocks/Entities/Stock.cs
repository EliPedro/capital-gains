using CapitalGain.Features.Stocks.Validators;

namespace CapitalGain.Features.Stocks.Entities
{
    public sealed class Stock
    {
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal WeightedAverage { get; private set; }
        public decimal AccumulatedLoss { get; private set; }

        public Stock(string name)
        {
            Name = name;
            Quantity = 0;
            WeightedAverage = 0;
            AccumulatedLoss = 0;
        }

        public void CalculateWeightedAverage(int quantity, decimal purchasePrice)
        {
            WeightedAverage = (Quantity * WeightedAverage + quantity * purchasePrice) / (Quantity + quantity);
            UpdateQuantity(quantity);
        }

        private void UpdateQuantity(int quantity) => Quantity += quantity;

        public decimal CalculateTax(int quantity, decimal sellingPrice)
        {
            if (quantity > Quantity)
                throw new StockException("The quantity of shares to sell is greater than the available quantity.");

            decimal totalSale = quantity * sellingPrice;
            decimal totalCost = quantity * WeightedAverage;
            decimal profit = totalSale - totalCost;

            if (IsProfitNegative(profit))
            {
                AddAccumulatedLoss(profit);
                Quantity -= quantity;
                return NoTaxToPay();
            }

            decimal tax = 0.0m;

            if (totalSale > TaxLimit())
            {
                if (DeductAccumulatedLoss())
                {
                    if (AccumulatedLoss >= profit)
                    {
                        AccumulatedLoss -= profit;
                        profit = 0.0m;
                    }
                    else
                    {
                        profit -= AccumulatedLoss;
                        ResetAccumulatedLoss();
                    }
                }
                tax = CalculateTaxOnProfit(profit, ProfitPercentage());
            }

            Quantity -= quantity;
            return tax;
        }

        private decimal ProfitPercentage() => 0.20m;

        private decimal TaxLimit() => 20000m;

        public bool DeductAccumulatedLoss() => AccumulatedLoss > 0.0m;

        public decimal NoTaxToPay() => 0.0m;

        public void AddAccumulatedLoss(decimal profit) => AccumulatedLoss += -profit;

        public void ResetAccumulatedLoss() => AccumulatedLoss = 0.0m;

        private bool IsProfitNegative(decimal profit) => profit < 0.0m;

        public decimal CalculateTaxOnProfit(decimal profit, decimal profitPercentage) => profit * profitPercentage;
    }
}