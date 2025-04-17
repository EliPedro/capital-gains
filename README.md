# Capital Gains - C# Application

This project is a C# application that implements the rules for capital gains related to the buying and selling of stocks. The application uses design patterns to organize the code and facilitate maintenance.

## Code Structure

### Interfaces and Classes for Operations
- **IOperation**: Interface that defines the `Execute` method.
- **BuyOperation**: Implementation that performs the buying operation of stocks.
- **SellOperation**: Implementation that performs the selling operation of stocks.

### Stock Class
The `Stock` class represents a stock with the following properties:
- **Name**: The name of the stock.
- **Quantity**: The quantity of available stocks.
- **WeightedAverage**: The weighted average price of the stocks.
- **AccumulatedLoss**: The accumulated loss from previous sales.

The class has methods to:
- **CalculateWeightedAverage**: Updates the quantity of stocks and calculates the new weighted average.
- **CalculateTax**: Calculates the profit or loss from the sale and determines if there is tax to be paid.

### Operation Factory
- **OperationFactory**: Class responsible for creating instances of operations (`BuyOperation` and `SellOperation`) based on the provided type.

## Getting Started

### Prerequisites
- .NET 8.0 or higher
- Any IDE or text editor (e.g., Visual Studio, Visual Studio Code)

### Rules 

new-weighted-average = ((current-stock-quantity * current-weighted-average) + (purchased-stock-quantity * purchase-price)) / (current-stock-quantity + purchased-stock-quantity)

For example, if you bought 10 stocks at $20.00, sold 5, and then bought another 5 stocks at $10.00, the weighted average is:
((5 x 20.00) + (5 x 10.00)) / (5 + 5) = 15.00

### How to use
Navigating to project directory and run the following command to build the project:

dotnet build
dotnet run --project src/App/CapitalGain.csproj

## Give a file path like C:\\CapitalGain.txt

## Input
[
  [{"operation":"buy","unit-cost":10.00,"quantity":10000},{"operation":"sell","unit-cost":20.00,
  "quantity":5000},{"operation":"sell","unit-cost":5.00,"quantity":5000}]
]

## Output
[[{"tax":0.0},{"tax":10000.0},{"tax":0.0}]]