using AvDe.Contracts.Models.Statistics;
using AvDe.StatisticsDashboard.Models;
using AvDe.StatisticsDashboard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AvDe.StatisticsDashboard.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAvDeWebApiClient _avDeWebApiClient;

        public IndexModel(IAvDeWebApiClient avDeWebApiClient)
        {
            _avDeWebApiClient = avDeWebApiClient;
        }
       
        public List<RevenuePerDay> RevenuePerDay { get; private set; }

        public List<SoldArticlesPerDay> SoldArticlesPerDay { get; private set; }

        public List<RevenuePerOrder> Top10OrdersByRevenue { get; private set; }

        public List<SoldArticle> Top10SoldArticles { get; private set; }

        public void OnGet()
        {
            RevenuePerDay = _avDeWebApiClient.GetRevenuePerDay().Result;
            SoldArticlesPerDay = _avDeWebApiClient.GetSoldArticlesPerDay().Result;
            Top10OrdersByRevenue = _avDeWebApiClient.GetTop10OrdersByRevenue().Result;
            Top10SoldArticles = _avDeWebApiClient.GetTop10SoldArticles().Result;
        }

        public JsonResult OnGetStatisticsChartData()
        {
            var statisticsChartModel = new StatisticsChartModel
            {
                RevenuePerDayDates = new List<string>(),
                RevenuePerDayTotalSalesAmount = new List<decimal>(),
                SoldArticlesPerDay_Dates = new List<string>(),
                SoldArticlesPerDay_TotalSalesQuantity = new List<long>(),
                RevenuePerOrder_DatePlaced = new List<string>(),
                RevenuePerOrder_TotalSalesAmount = new List<decimal>(),
                Top10SoldArticles_ArticleName = new List<string>(),
                Top10SoldArticles_TotalSalesAmount = new List<decimal>()
            };

            var revenuePerDay = _avDeWebApiClient.GetRevenuePerDay().Result;
            foreach (var day in revenuePerDay)
            {
                statisticsChartModel.RevenuePerDayDates.Add(day.Date.ToString("dd.MM.yyyy"));
                statisticsChartModel.RevenuePerDayTotalSalesAmount.Add(day.TotalSalesAmount);
            }

            var soldArticlesPerDay = _avDeWebApiClient.GetSoldArticlesPerDay().Result;
            foreach (var day in soldArticlesPerDay)
            {
                statisticsChartModel.SoldArticlesPerDay_Dates.Add(day.Date.ToString("dd.MM.yyyy"));
                statisticsChartModel.SoldArticlesPerDay_TotalSalesQuantity.Add(day.TotalSalesQuantity);
            }

            var top10OrdersByRevenue = _avDeWebApiClient.GetTop10OrdersByRevenue().Result;
            foreach (var order in top10OrdersByRevenue)
            {
                statisticsChartModel.RevenuePerOrder_DatePlaced.Add(order.DatePlaced.ToString("dd.MM.yyyy"));
                statisticsChartModel.RevenuePerOrder_TotalSalesAmount.Add(order.TotalSalesAmount);
            }

            var top10SoldArticles = _avDeWebApiClient.GetTop10SoldArticles().Result;
            foreach (var article in top10SoldArticles)
            {
                statisticsChartModel.Top10SoldArticles_ArticleName.Add(article.ArticleName);
                statisticsChartModel.Top10SoldArticles_TotalSalesAmount.Add(article.TotalSalesAmount);
            }

            var result = new JsonResult(statisticsChartModel);
            return result;
        }
    }
}
