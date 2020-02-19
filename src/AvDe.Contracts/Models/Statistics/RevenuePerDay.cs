using Newtonsoft.Json;
using System;

namespace AvDe.Contracts.Models.Statistics
{
    /// <summary>
    /// Represents a sold article
    /// </summary>
    [Serializable]
    [JsonObject(IsReference = false)]
    public class RevenuePerDay
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("totalSalesAmount")]
        public decimal TotalSalesAmount { get; set; }
    }
}