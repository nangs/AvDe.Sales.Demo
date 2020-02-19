using Newtonsoft.Json;
using System;

namespace AvDe.Contracts.Models.Statistics
{
    /// <summary>
    /// Represents a sold article
    /// </summary>
    [Serializable]
    [JsonObject(IsReference = false)]
    public class SoldArticle
    {
        [JsonProperty("articleId")]
        public string ArticleId { get; set; }

        [JsonProperty("articleName")]
        public string ArticleName { get; set; }

        [JsonProperty("totalSalesAmount")]
        public decimal TotalSalesAmount { get; set; }
    }
}