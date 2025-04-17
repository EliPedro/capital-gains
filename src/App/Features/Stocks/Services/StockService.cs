using CapitalGain.Core.FileProcessor;
using CapitalGain.Features.Stocks.Entities;
using CapitalGain.Features.Stocks.Factories;
using CapitalGain.Features.Stocks.Inputs;
using CapitalGain.Features.Stocks.Outputs;
using System.Text;
using System.Text.Json;

namespace CapitalGain.Features.Entities.Stocks.Services
{
    public class StockService : IStockService
    {
        private readonly IReader reader;

        public StockService(IReader reader)
        {
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public async Task ExecuteAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("File path cannot be empty.");
                return;
            }

            var readerResult = await reader.ReadAsync(filePath);

            if (readerResult.Error != null)
            {
                Console.WriteLine($"Error: {readerResult.Error.Title}");
                Console.WriteLine($"Detail: {readerResult.Error.Detail}");
                return;
            }

            var operations = JsonSerializer.Deserialize<IList<IList<Operation>>>(readerResult.Content);

            var taxes = new StringBuilder();
            var listOfLists = new List<IList<TaxValue>>();

            foreach (var operationList in operations!)
            {
                var values = new List<TaxValue>();

                var stock = new Stock(_generateRandomCode(6));

                foreach (var operation in operationList)
                {
                    decimal tax = OperationFactory
                    .CreateOperation(operation.Type)
                    .Execute(stock, quantity: operation.Quantity, price: operation.UnitCost);

                    values.Add(new TaxValue(tax));
                }

                listOfLists.Add(values);
            }

            Console.WriteLine(JsonSerializer.Serialize(listOfLists));
        }

        private static string _generateRandomCode(int maxLength)
        {
            Random random = new();
            const string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int length = random.Next(maxLength, maxLength + 1);
            StringBuilder result = new(length);

            for (int i = 0; i < length; i++)
            {
                char randomCharacter = allowedCharacters[random.Next(allowedCharacters.Length)];
                result.Append(randomCharacter);
            }

            return result.ToString();
        }
    }
}