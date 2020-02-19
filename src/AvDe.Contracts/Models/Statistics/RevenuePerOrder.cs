using Newtonsoft.Json;
using System;

namespace AvDe.Contracts.Models.Statistics
{
    /// <summary>
    /// Represents a RevenuePerOrder
    /// </summary>
    [Serializable]
    [JsonObject(IsReference = false)]
    public class RevenuePerOrder
    {
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        [JsonProperty("datePlaced")]
        public DateTime DatePlaced { get; set; }

        [JsonProperty("totalSalesAmount")]
        public decimal TotalSalesAmount { get; set; }
    }
}