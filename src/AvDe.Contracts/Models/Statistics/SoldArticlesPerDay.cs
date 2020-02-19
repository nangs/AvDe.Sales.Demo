using Newtonsoft.Json;
using System;

namespace AvDe.Contracts.Models.Statistics
{
    /// <summary>
    /// Represents a SoldArticlesPerDay
    /// </summary>
    [Serializable]
    [JsonObject(IsReference = false)]
    public class SoldArticlesPerDay
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("totalSalesQuantity")]
        public long TotalSalesQuantity { get; set; }
    }
}