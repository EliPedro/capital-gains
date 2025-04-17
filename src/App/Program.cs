using CapitalGain.Core.FileProcessor;
using CapitalGain.Features.Entities.Stocks.Services;

try
{
    Console.WriteLine("Please enter the file path: Example: C:\\CapitalGain.txt");
    var filePath = Console.ReadLine();

    IStockService service = new StockService(new Reader());
    _ = service.ExecuteAsync(filePath);
}
catch (Exception e)
{
    Console.WriteLine($"{e.Message}");
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();