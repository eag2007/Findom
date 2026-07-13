namespace free.Common
{
    /// <summary>
    /// Структура содержащая имя источника (name), ссылку на Rss (urlRss), ссылку на Robots.txt (urlRobots)
    /// </summary>
    public readonly struct Feed
    {
        public readonly string Name { get; }
        public readonly string UrlRss { get; }

        public readonly string UrlRobots { get; }

        public Feed(string name, string urlRss, string urlRobots)
        {
            this.Name = name;
            this.UrlRss = urlRss;
            this.UrlRobots = urlRobots;
        }
    }

    /// <summary>
    /// Класс хранящий объекты Feed
    /// </summary>
    public static class Urls
    {
        private static readonly List<Feed> RssFeed = new()
        {
            new Feed("ТАСС", "https://tass.ru/rss/v2.xml", "https://tass.ru/robots.txt")
        };

        /// <summary>
        /// Геттер для всей информации
        /// </summary>
        /// <returns>Возвращает список имён, rss-urls, robots-urls</returns>
        public static List<Feed> GetFeeds() => RssFeed;

        /// <summary>
        /// Геттер для url-rss
        /// </summary>
        /// <returns>Возвращает список ссылок rss</returns>
        public static List<string> GetUrlsRss() => RssFeed.Select(feed => feed.UrlRss).ToList();

        /// <summary>
        /// Геттер для url-robots
        /// </summary>
        /// <returns>Возвращает список ссылок robots</returns>
        public static List<string> GetUrlsRobots() => RssFeed.Select(feed => feed.UrlRobots).ToList();
    }
}