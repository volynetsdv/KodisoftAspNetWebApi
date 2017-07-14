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
        //usage:
        //FeedParser parser = new FeedParser();
        //var items = parser.Parse("http://www.ft.com/rss/home/uk", FeedType.RSS);
        public void Parsing(int id) //требуется входящий параметр с индексом элемента
        {
            var optionsBuilder = new DbContextOptionsBuilder<FeedsContext>();
            var options = optionsBuilder
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=usersdbstore;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            var db = new FeedsContext(options);
            var parser = new FeedParser();


            //var listOfUrls = .UrlsList.Select(r => r.Id);
            //var requestUrl = db.UrlsList.Where(r => listOfUrls.Contains(r.Id));


            var requestUrl = db.UrlsList.ElementAtOrDefault(id).FeedUrl; 

            FeedType resourceTipe;
            if (requestUrl.Contains("rss"))
            { resourceTipe = FeedType.RSS;}
            else
            { resourceTipe = FeedType.Atom;}

            var news = parser.Parse(requestUrl, resourceTipe);
            foreach (var n in news)
            {
                db.Add(n);
            }
        }
    }
}
