using AvDe.Contracts.Models.Statistics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvDe.StatisticsDashboard.Services
{
    public interface IAvDeWebApiClient
    {
        Task<List<SoldArticle>> GetTop10SoldArticles();

        Task<List<SoldArticlesPerDay>> GetSoldArticlesPerDay();

        Task<List<RevenuePerDay>> GetRevenuePerDay();

        Task<List<RevenuePerOrder>> GetTop10OrdersByRevenue();
    }
}
