using System.Text.Json.Serialization;

namespace CapitalGain.Features.Stocks.Outputs
{
    public class TaxValue(decimal value)
    {
        [JsonPropertyName("tax")]
        public decimal Value { get; set; } = value;
    }
}