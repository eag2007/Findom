using Abot2.Crawler;
using Abot2.Poco;
using Crawler.Context;
using Crawler.Controllers;
using Crawler.Entities;
using Serilog;
using System.CommandLine;

namespace Crawler
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.Information()
                .CreateLogger();

            new FindomContext().Database.EnsureCreated();

            const string URL = "https://ru.wikipedia.org/wiki/%D0%97%D0%B0%D0%B3%D0%BB%D0%B0%D0%B2%D0%BD%D0%B0%D1%8F_%D1%81%D1%82%D1%80%D0%B0%D0%BD%D0%B8%D1%86%D0%B0";
            Option<string> urlToCrawlOption = new("--url", "-u")
            {
                Description = "The URL to crawl starting",
                DefaultValueFactory = _ => URL
            };

            Option<int> maxCountToCrawlOption = new("--max-count", "-m")
            {
                Description = "The maximum number of pages to crawl"
            };

            RootCommand rootCommand = new("Simple Web Crawler");
            rootCommand.Options.Add(urlToCrawlOption);
            rootCommand.Options.Add(maxCountToCrawlOption);

            rootCommand.SetAction(async parseResult =>
            {
                string urlToCrawl = parseResult.GetValue(urlToCrawlOption);
                Log.Information("Starting crawl for URL: {Url}", urlToCrawl);

                int? maxCountOfPages = parseResult.GetValue(maxCountToCrawlOption);
                if (maxCountOfPages is not null)
                {
                    await Crawler(urlToCrawl, (int)maxCountOfPages);
                }
                else
                {
                    await Crawler(URL);

                }
            });

            ParseResult parseResult = rootCommand.Parse(args);
            return parseResult.InvokeAsync().Result;
        }

        private static async Task Crawler(string url)
        {
            var crawler = new PoliteWebCrawler();

            crawler.PageCrawlCompleted += (sender, e) =>
            {
                var crawledPage = e.CrawledPage;
                Log.Logger.Information($"Crawled page: {crawledPage.Uri.AbsoluteUri}");
                DbManager.AddUrlToQueue(crawledPage.Uri.AbsoluteUri);
            };

            var crawlResult = await crawler.CrawlAsync(new Uri(url));
        }

        private static async Task Crawler(string url, int maxCountToCrawl)
        {
            var config = new CrawlConfiguration
            {
                MaxPagesToCrawl = maxCountToCrawl,
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
