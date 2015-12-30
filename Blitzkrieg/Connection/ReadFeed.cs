using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Blitzkrieg.Connection
{
    public class ReadFeed
    {
        public List<Post> ParseFeed(string url)
        {
            try
            {
                List<Post> p = null;
                DownloadFeed down = new DownloadFeed();

                using (XmlReader r = down.DownloadFeedFromUrl(url))
                {
                    var rssFeed = XDocument.Load(r);

                    p = (from item in rssFeed.Descendants("item")
                         select new Post
                         {
                             Title = item.Element("title").Value,
                             Link = item.Element("link").Value,
                             Description = item.Element("description").Value,
                             PublishedDate = item.Element("pubDate").Value
                         }).ToList();

                    r.Close();
                }

                down = null;

                return p;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
