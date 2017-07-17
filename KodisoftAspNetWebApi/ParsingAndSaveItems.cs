using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KodisoftAspNetWebApi.Controllers;
using KodisoftAspNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KodisoftAspNetWebApi
{
    public class ParsingAndSaveItems
    {
        public void Parsing(int id) 
        {
            var parser = new FeedParser();
            
            //get RSS/Atom URL for parser
            var UrlsList = FeedsController.db.UrlsList.ToList();  //<<<<<<<EXCEPTION!!!
            var requestUrl;
            foreach (var url in UrlsList)
            {
                if (url.Id.Equals(id))
                {
                    requestUrl = url;
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
    }
}
