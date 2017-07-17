using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KodisoftAspNetWebApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KodisoftAspNetWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        public FeedsContext db;
        public ItemsController(FeedsContext context)
        {
            db = context;
        }
        //Get all news (isn't ready)
        [HttpGet]
        [ActionName("GetNewsList")]
        public IEnumerable<FeedItem> Get()
        {
            return db.Items.ToList();
        }

        //Get news by ID
        [HttpGet("{id}")]
        [ActionName("GetNews")]
        public IActionResult Get(int id)
        {
            var parsing = new ParsingAndSaveItems();
            parsing.Parsing(id);


            //FeedList feedList = db.UrlsList.FirstOrDefault(x => x.Id == id);
            FeedItem feedItem = db.Items.FirstOrDefault(x => x.FeedUrlId == id);
            if (feedItem == null)
                return NotFound();
            return new ObjectResult(feedItem);
        }

        ////Get news by SubGroup
        //[HttpGet("{id}")]
        //[ActionName("getnews-for-subgroupe")]
        //public IActionResult Get(string subgroupe)
        //{
        //    var parsing = new ParsingAndSaveItems();
        //    parsing.Parsing(id);


        //    //FeedList feedList = db.UrlsList.FirstOrDefault(x => x.Id == id);
        //    FeedItem feedItem = db.Items.FirstOrDefault(x => x.FeedUrlId == id);
        //    if (feedItem == null)
        //        return NotFound();
        //    return new ObjectResult(feedItem);
        //}



        ////does not finished part
        //// POST api/feeds
        //[HttpPost]
        //public IActionResult Post([FromBody]FeedItem item)
        //{
        //    if (item == null)
        //    {
        //        ModelState.AddModelError("", "Не указаны данные для пользователя");
        //        return BadRequest(ModelState);
        //    }
        //    // если есть ошибки - возвращаем ошибку 400
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    // если ошибок нет, сохраняем в базу данных
        //    db.Items.Add(item);
        //    db.SaveChanges();
        //    return Ok(item);
        //}

        //// PUT api/feeds/
        //[HttpPut]
        //public IActionResult Put([FromBody]FeedList item)
        //{
        //    if (item == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.UrlsList.Any(x => x.Id == item.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(item);
        //    db.SaveChanges();
        //    return Ok(item);
        //}

        //// DELETE api/feeds/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    FeedList item = db.UrlsList.FirstOrDefault(x => x.Id == id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }
        //    db.UrlsList.Remove(item);
        //    db.SaveChanges();
        //    return Ok(item);
        //}
    }
}
