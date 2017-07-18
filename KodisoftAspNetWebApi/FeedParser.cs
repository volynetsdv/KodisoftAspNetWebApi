using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using KodisoftAspNetWebApi.Models;

namespace KodisoftAspNetWebApi
{
    // about RSS: https://cyber.harvard.edu/rss/rss.html#lttextinputgtSubelementOfLtchannelgt
    // about parsing: http://www.anotherchris.net/csharp/simplified-csharp-atom-and-rss-feed-parser/


    /// <summary>
    /// RSS and ATOM feed parser.
    /// </summary>
    public class FeedParser
    {
        /// <summary>
        /// Parses the given <see cref="FeedType"/> and returns a <see cref="IList&amp;lt;Item&amp;gt;"/>.
        /// </summary>
        /// <returns></returns>
        public IList<FeedItem> Parse(string url, FeedType feedType)
        {
            switch (feedType)
            {
                case FeedType.RSS:
                    return ParseRss(url);
                case FeedType.Atom:
                    return ParseAtom(url);
                default:
                    throw new NotSupportedException(string.Format("{0} is not supported", feedType.ToString()));
            }
        }

        /// <summary>
        /// Parses an Atom feed and returns a <see cref="IList&amp;lt;Item&amp;gt;"/>.
        /// </summary>
        public virtual IList<FeedItem> ParseAtom(string url)
        {
            try
            {
                XDocument doc = XDocument.Load(url);
                // Feed/Entry
                var entries = from item in doc.Root.Elements().Where(i => i.Name.LocalName == "entry")
                    select new FeedItem
                    {
                        FeedType = FeedType.Atom,
                        Description = item.Elements().First(i => i.Name.LocalName == "summary").Value,
                        ResourceUrl = item.Elements().First(i => i.Name.LocalName == "link").Attribute("href").Value,
                        UpdatedOn = ParseDate(item.Elements().First(i => i.Name.LocalName == "updated").Value),
                        Title = item.Elements().First(i => i.Name.LocalName == "title").Value,
                        Image = item.Elements().First(i => i.Name.LocalName == "link").Element("image/jpeg").Attribute("href").Value //не уверен в корректности - Element("image/jpeg")
                    };
                return entries.ToList();
            }
            catch
            {
                return new List<FeedItem>();
            }
        }

        /// <summary>
        /// Parses an RSS feed and returns a <see cref="IList&amp;lt;Item&amp;gt;"/>.
        /// </summary>
        public virtual IList<FeedItem> ParseRss(string url)
        {
            //try
            //{
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var responseMessage = client.GetAsync(url).GetAwaiter().GetResult();
                var responseString = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                //extract feed items
                XDocument doc = XDocument.Parse(responseString);
                // RSS/Channel/item
                var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements()
                        .Where(i => i.Name.LocalName == "item")
                    select new FeedItem
                    {                        
                        Title = item.Elements().First(i => i.Name.LocalName == "title").Value,
                        ResourceUrl = item.Elements().First(i => i.Name.LocalName == "link").Value,
                        Description = item.Elements().First(i => i.Name.LocalName == "description").Value,
                        UpdatedOn = ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
                        //FeedType = FeedType.RSS,
                        //Image =
                        //    item.Elements().First(i => i.Name.LocalName == "img").Attribute("src")
                        //        .Value //не уверен в корректности указанных входящих параметров
                    };
                return entries.ToList();
            }
            //}
            //catch
            //{
            //    return new List<FeedItem>();
            //}
        }

        private DateTime ParseDate(string date)
        {
            DateTime result;
            if (DateTime.TryParse(date, out result))
                return result;
            else
                return DateTime.MinValue;
        }
    }

    /// <summary>
    /// Represents the XML format of a feed.
    /// </summary>
    public enum FeedType
    {
        /// <summary>
        /// Really Simple Syndication format.
        /// </summary>
        RSS,
        /// <summary>
        /// Atom Syndication format.
        /// </summary>
        Atom
    }
}