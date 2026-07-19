using Crawler.Context;
using Crawler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Controllers
{
    internal class DbManager
    {
        public static bool AddUrlToQueue(string url)
        {
            FindomContext context = new FindomContext();
            if (context.RawLinks.All(r => r.Url != url))
            {
                RawLink newData = new RawLink
                {
                    Url = url
                };
                context.Add(newData);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
