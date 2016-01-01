using HtmlAgilityPack;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Blitzkrieg.Connection
{
    public class DownloadFeed
    {
        public string DownloadFeedFromUrl(string uri)
        {
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(uri);

                req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:43.0) Gecko/20100101 Firefox/43.0";
                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                req.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                req.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
                
                var resp = req.GetResponse();
                var stream = DeflateGZipStream(resp.GetResponseStream());

                var data = new StreamReader(stream, Encoding.UTF8);
                var text = Regex.Replace(data.ReadToEnd(), "&(?!amp;)", "&amp;");

                return text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Downloads a Favicon from any website.
        /// <para>It searchs and downloads only the first Favicon entry.</para>
        /// </summary>
        /// <param name="uri">Any Valid URL</param>
        /// <returns>Byte array Base64 Encoded Image or Icon</returns>
        public string DownloadFavicon(string uri)
        {
            var type = uri.Substring(0, uri.IndexOf("//") + 2);
            var newUri = uri.Substring(uri.IndexOf("//") + 2);
                newUri = newUri.Substring(0, newUri.IndexOf("/")) + "/";

            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(type + newUri);
            foreach (HtmlNode link in doc.DocumentNode.Descendants("link"))
            {
                if (link.GetAttributeValue("rel", null).Contains("icon"))
                {
                    var favicon = link.GetAttributeValue("href", null);
                    if(favicon != null)
                    {
                        var client = new WebClient();

                        if (favicon.Contains("http"))
                            return Convert.ToBase64String(client.DownloadData(favicon));
                        else
                            return Convert.ToBase64String(client.DownloadData(type + newUri + favicon));
                    }
                }
            }

            return string.Empty;
        }

        public Stream DeflateGZipStream(Stream stream)
        {
            try
            {
                return new GZipStream(stream, CompressionMode.Decompress, true);
            }
            catch
            {
                return stream;
            }
        }

        public byte[] StreamToByteArr(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
