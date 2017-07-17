using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KodisoftAspNetWebApi.Controllers;
using KodisoftAspNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KodisoftAspNetWebApi
{    
    /// <summary>
    /// Parsing feeds and save in database.
    /// </summary>
    public class ParsingAndSaveItems
    {
        /// <summary>
        /// Parsing and saving one feed.
        /// </summary>
        public void Parsing(int id) 
        {
            var parser = new FeedParser();
            
            //get RSS/Atom URL for parser
            var UrlsList = FeedsController.db.UrlsList.ToList();  //<<<<<<<EXCEPTION!!!
            string requestUrl = null;
            foreach (var url in UrlsList)
            {
                if (url.Id.Equals(id))
                {
                    requestUrl = url.FeedUrl;
                    break;
                }
            }

            FeedType resourceTipe;
            if (requestUrl.Contains("rss"))
            { resourceTipe = FeedType.RSS;}
            else
            { resourceTipe = FeedType.Atom;}

            //parse list of news
            var news = parser.Parse(requestUrl, resourceTipe);

            //save each eltmtnts to SQL
            foreach (var n in news)
            {
                FeedsController.db.Add(n);
            }
        }
        /// <summary>
        /// Parsing and saving data for group of user feeds.
        /// </summary>
        public void Parsing(string subGroupe)
        {
            var parser = new FeedParser();

            //get RSS/Atom URL for parser
            var urlsList = FeedsController.db.UrlsList.ToList();  
            List<FeedList> parsingList = null;
            foreach (var url in urlsList)
            {
                if (url.SubGroup.Equals(subGroupe))
                {
                    parsingList.Add(url);
                }
            }
            foreach (var pars in parsingList)
            {
                //FeedType resourceTipe;
                if (pars.FeedUrl.Contains("rss"))
                {
                    var resourceTipe = FeedType.RSS;
                    var news = parser.Parse(pars.FeedUrl, resourceTipe);
                    foreach (var n in news)
                    {
                        FeedsController.db.Add(n);
                    }
                }
                else
                {
                    var resourceTipe = FeedType.Atom;
                    var news = parser.Parse(pars.FeedUrl, resourceTipe);
                    foreach (var n in news)
                    {
                        FeedsController.db.Add(n);
                    }
                }

            }

        }
    }
}
