using KodisoftAspNetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KodisoftAspNetWebApi.Controllers;

namespace KodisoftAspNetWebApi
{
    public class DbItemsUpdater
    {
        public void DbItemsUpdater()
        {
                // создаем два объекта User
                User item = new User {Name = "Tom", Age = 33};

                // добавляем в бд
                FeedsController.db.Items.Add(item);
                FeedsController.db.SaveChanges();

                // получаем объекты из бд и выводим на консоль
                var users = FeedsController.db.Users.ToList();

            
        }
    }
}
