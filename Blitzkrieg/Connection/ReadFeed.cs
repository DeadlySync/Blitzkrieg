using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Blitzkrieg.Connection
{
    public class ReadFeed
    {
        public List<Post> ParseFeed(string feedAlias, string url)
        {
            try
            {
                List<Post> p = null;
                DownloadFeed down = new DownloadFeed();
                var data = down.DownloadFeedFromUrl(url);

                var rssFeed = XDocument.Parse(data);

                p = (from item in rssFeed.Descendants("item")
                     select new Post
                     {
                         Title = item.Element("title").Value,
                         Link = item.Element("link").Value,
                         Magnet = ParseCDATA(item.Element("description").Value),
                         PublishedDate = item.Element("pubDate").Value,
                         FromFeed = feedAlias,
                         Done = "0.0%"
                     }).ToList();

                down = null;

                return p;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ParseCDATA(string data)
        {
            if (!data.Contains("magnet"))
                return string.Empty;

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(data);
            HtmlNode nodes = doc.DocumentNode;

            return nodes.Descendants("a").LastOrDefault().Attributes["href"].Value;
        }
    }
}
