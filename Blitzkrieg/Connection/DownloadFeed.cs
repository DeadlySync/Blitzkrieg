using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Xml;

namespace App.Connection
{
    public class DownloadFeed
    {
        public XmlReader DownloadFeedFromUrl(string uri)
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

                var xmlSettings = new XmlReaderSettings();
                xmlSettings.DtdProcessing = DtdProcessing.Parse;
                
                return XmlReader.Create(stream, xmlSettings);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
    }
}
