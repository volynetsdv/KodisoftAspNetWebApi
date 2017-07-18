using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KodisoftAspNetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KodisoftAspNetWebApi
{
    public class SetDataToDb
    {
        public void SetDataToDataBase(IList<FeedItem> list)
        {
            using (FeedsContext db = new FeedsContext())
            {
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        db.Items.Add(item);
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}
