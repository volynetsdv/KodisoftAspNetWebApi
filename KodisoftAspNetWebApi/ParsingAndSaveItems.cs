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
        GetDataFromDb urlsList = new GetDataFromDb();

        /// <summary>
        /// Parsing and saving one feed.
        /// </summary>
        public void Parsing(int id)
        {
            var parser = new FeedParser();
            List<FeedList> urls = urlsList.GetDataFromDataBase();


            if (urls != null)
            {
                //get RSS/Atom URL for parser
                //var urlsList = db.UrlsList.ToList(); //<<<<<<<EXCEPTION!!!
                string requestUrl = null;
                foreach (var url in urls)
                {
                    if (url.Id.Equals(id))
                    {
                        requestUrl = url.FeedUrl;
                        break;
                    }
                }

                if (requestUrl != null)
                {
                    FeedType resourceTipe;
                    if (requestUrl.Contains("rss"))
                    {
                        resourceTipe = FeedType.RSS;
                    }
                    else
                    {
                        resourceTipe = FeedType.Atom;
                    }

                    //parse list of news
                    var news = parser.Parse(requestUrl, resourceTipe);

                    //save each eltmtnts to SQL
                    if (news != null)
                    {
                        var set = new SetDataToDb();
                        set.SetDataToDataBase(news);
                    }
                }
            }
        }

        /// <summary>
        /// Parsing and saving data for group of user feeds.
        /// </summary>
        public void Parsing(string subGroup)
        {
            var parser = new FeedParser();
            List<FeedList> urls = urlsList.GetDataFromDataBase();

            if (urls != null)
            {
                //get RSS/Atom URL for parser
                List<FeedList> parsingList = null;
                foreach (var url in urls)
                {
                    if (url.SubGroup.Equals(subGroup))
                    {
                        parsingList.Add(url);
                    }
                }
                if (parsingList != null)
                    foreach (var pars in parsingList)
                    {
                        //FeedType resourceTipe;
                        if (pars.FeedUrl.Contains("rss"))
                        {
                            var resourceTipe = FeedType.RSS;
                            var news = parser.Parse(pars.FeedUrl, resourceTipe);
                            //save each eltmtnts to SQL
                            if (news != null)
                            {
                                var set = new SetDataToDb();
                                set.SetDataToDataBase(news);
                            }
                        }
                        else
                        {
                            var resourceTipe = FeedType.Atom;
                            var news = parser.Parse(pars.FeedUrl, resourceTipe);
                            //save each eltmtnts to SQL
                            if (news != null)
                            {
                                var set = new SetDataToDb();
                                set.SetDataToDataBase(news);
                            }
                        }

                    }
            }

        }
    }
}
