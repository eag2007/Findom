using System.ServiceModel.Syndication;
using System.Xml;

namespace findom.Crawler
{
    /// <summary>
    /// Класс, который собирают Rss ленты с сайтов
    /// </summary>
    public class RssFeatcher
    {
        /// <summary>
        /// Считывает Rss ленту с сайта
        /// </summary>
        /// <param name="url">Адрес Rss страницы сайта</param>
        public void ReadPageRss(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            foreach (var rssItem in feed.Items)
            {   
                Console.WriteLine("---------------------------------");
                Console.WriteLine(rssItem.Title?.Text ?? "-");
                Console.WriteLine(rssItem.Summary?.Text ?? "-");
                Console.WriteLine(rssItem.Id ?? "-");
                Console.WriteLine(rssItem.PublishDate.DateTime);
                foreach (var _linnk in rssItem.Links)
                {
                    Console.WriteLine(_linnk.GetAbsoluteUri());
                }

                foreach (var _categories in rssItem.Categories)
                {
                    Console.WriteLine(_categories.Name ?? "-");
                    Console.WriteLine(_categories.Label ?? "-");
                    Console.WriteLine(_categories.Scheme ?? "-");
                }

                foreach (var _authors in rssItem.Authors) 
                {
                    Console.WriteLine(_authors);
                }
            }
        }
    }
}