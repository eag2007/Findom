using System.ServiceModel.Syndication;
using System.Xml;

namespace free.Crawler
{
    public class RssFeatcher
    {
        public void ReadRssToObject(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            foreach (var rssItem in feed.Items)
            {
                Console.WriteLine(rssItem.Title.Text);
                foreach (var _linnk in rssItem.Links)
                {
                    Console.WriteLine(_linnk.GetAbsoluteUri());
                }
                
            }
        }
    }
}