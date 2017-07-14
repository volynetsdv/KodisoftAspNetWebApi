using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KodisoftAspNetWebApi.Controllers;
using KodisoftAspNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KodisoftAspNetWebApi
{
    public class ParsingAndSaveFeeds
    {
        public void Parsing(int id) 
        {
            var optionsBuilder = new DbContextOptionsBuilder<FeedsContext>();
            var options = optionsBuilder
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=usersdbstore;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;
            var db = new FeedsContext(options);
            var parser = new FeedParser();

            //get RSS/Atom URL for parser
            var requestUrl = db.UrlsList.ElementAtOrDefault(id).FeedUrl;  //<<<<<<<EXCEPTION!!!

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
                db.Add(n);
            }
        }
    }
}
