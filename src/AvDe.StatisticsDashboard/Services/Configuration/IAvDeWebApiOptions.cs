namespace AvDe.StatisticsDashboard.Services.Configuration
{
    public interface IAvDeWebApiOptions
    {
        string BaseUrl { get; set; }

        string Top10SoldArticlesUrl { get; set; }

        string SoldArticlesPerDayUrl { get; set; }

        string RevenuePerDayUrl { get; set; }

        string Top10OrdersByRevenueUrl { get; set; }

        int HttpClientTimeoutSeconds { get; set; }
    }
}