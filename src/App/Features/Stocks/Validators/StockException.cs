namespace CapitalGain.Features.Stocks.Validators;

public class StockException : Exception
{
    public StockException()
    {
    }

    public StockException(string message) : base(message)
    {
    }

    public StockException(string message, Exception inner) : base(message, inner)
    {
    }
}