using Abot2.Crawler;
using Abot2.Poco;
using Crawler.Controllers;
using Crawler.Entities;
using Serilog;

namespace Crawler
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            const string URL = "https://ru.wikipedia.org/wiki/%D0%97%D0%B0%D0%B3%D0%BB%D0%B0%D0%B2%D0%BD%D0%B0%D1%8F_%D1%81%D1%82%D1%80%D0%B0%D0%BD%D0%B8%D1%86%D0%B0";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.Information()
                .CreateLogger();

            Log.Logger.Information("Starting");

            await Crawler(URL);
        }

        private static async Task Crawler(string url)
        {
            var config = new CrawlConfiguration
            {
                MaxPagesToCrawl = 1000,
            };
            var crawler = new PoliteWebCrawler(config);

            crawler.PageCrawlCompleted += (sender, e) =>
            {
                var crawledPage = e.CrawledPage;
                Log.Logger.Information($"Crawled page: {crawledPage.Uri.AbsoluteUri}");
                DbManager.AddUrlToQueue(crawledPage.Uri.AbsoluteUri);
            };

            var crawlResult = await crawler.CrawlAsync(new Uri(url));
        }
    }
}
