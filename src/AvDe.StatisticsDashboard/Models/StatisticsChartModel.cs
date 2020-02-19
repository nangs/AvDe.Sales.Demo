using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AvDe.StatisticsDashboard.Models
{
    [Serializable]
    public class StatisticsChartModel
    {
        [JsonProperty(PropertyName = "RevenuePerDayDates")]
        public List<string> RevenuePerDayDates { get; set; }

        [JsonProperty(PropertyName = "RevenuePerDayTotalSalesAmount")]
        public List<decimal> RevenuePerDayTotalSalesAmount { get; set; }

        [JsonProperty(PropertyName = "SoldArticlesPerDay_Dates")]
        public List<string> SoldArticlesPerDay_Dates { get; set; }

        [JsonProperty(PropertyName = "SoldArticlesPerDay_TotalSalesQuantity")]
        public List<long> SoldArticlesPerDay_TotalSalesQuantity { get; set; }

        [JsonProperty("RevenuePerOrder_DatePlaced")]
        public List<string> RevenuePerOrder_DatePlaced { get; set; }

        [JsonProperty("RevenuePerOrder_TotalSalesAmount")]
        public List<decimal> RevenuePerOrder_TotalSalesAmount { get; set; }

        [JsonProperty("Top10SoldArticles_ArticleName")]
        public List<string> Top10SoldArticles_ArticleName { get; set; }

        [JsonProperty("Top10SoldArticles_TotalSalesAmount")]
        public List<decimal> Top10SoldArticles_TotalSalesAmount { get; set; }
    }
}
