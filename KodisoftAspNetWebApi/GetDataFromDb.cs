using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KodisoftAspNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KodisoftAspNetWebApi
{
    public class GetDataFromDb
    {
        public List<FeedList> GetDataFromDataBase()
        {
            using (FeedsContext db = new FeedsContext())
            {
                var urls = (from url in db.UrlsList
                    where url.FeedUrl != null
                    select url).ToList();

                return urls;
            }
        }
    }
}
