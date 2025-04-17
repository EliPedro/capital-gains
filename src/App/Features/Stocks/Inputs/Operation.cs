using System.Text.Json.Serialization;

namespace CapitalGain.Features.Stocks.Inputs
{
    public sealed class Operation
    {
        [JsonPropertyName("operation")]
        public string Type { get; set; }

        [JsonPropertyName("unit-cost")]
        public decimal UnitCost { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}