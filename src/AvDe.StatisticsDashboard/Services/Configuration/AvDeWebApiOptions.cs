namespace AvDe.StatisticsDashboard.Services.Configuration
{
    public class AvDeWebApiOptions : IAvDeWebApiOptions
    {
        public string BaseUrl { get; set; }

        public string Top10SoldArticlesUrl { get; set; }
        
        public string SoldArticlesPerDayUrl { get; set; }
        
        public string RevenuePerDayUrl { get; set; }

        public string Top10OrdersByRevenueUrl { get; set; }

        public int HttpClientTimeoutSeconds { get; set; } = 30;
    }
}