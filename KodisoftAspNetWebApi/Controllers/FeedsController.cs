using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KodisoftAspNetWebApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KodisoftAspNetWebApi.Controllers
{
    [Route("api/[controller]")]
    public class FeedsController : Controller
    {
        FeedsContext db;
        public FeedsController(FeedsContext context)
        {
            db = context;
            //if (!db.UrlsList.Any()) 
            //{
            //    db.UrlsList.Add(new FeedList { FeedUrl = @"http://receptculinar.ru/blog/atom", SubGroup = "news1" });
            //    db.UrlsList.Add(new FeedList { FeedUrl = @"http://ahier.ru/rss.xml", SubGroup = "news2" });
            //    db.SaveChanges();
            //}
        }

        [HttpGet]
        public IEnumerable<FeedList> Get()
        {
            return db.UrlsList.ToList();
        }

        // GET api/feeds/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            FeedList feedList = db.UrlsList.FirstOrDefault(x => x.Id == id);
            if (feedList == null)
                return NotFound();
            return new ObjectResult(feedList);
        }

        // POST api/feeds
        [HttpPost]
        public IActionResult Post([FromBody]FeedList url)
        {
            if (url == null)
            {
                ModelState.AddModelError("", "Не указан адрес ресурса");
                return BadRequest(ModelState);
            }
            // если есть ошибки - возвращаем ошибку 400
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // если ошибок нет, сохраняем в базу данных
            db.UrlsList.Add(url);
            db.SaveChanges();
            return Ok(url);
        }

        // PUT api/feeds/
        [HttpPut]
        public IActionResult Put([FromBody]FeedList url)
        {
            if (url == null)
            {
                return BadRequest();
            }
            if (!db.UrlsList.Any(x => x.Id == url.Id))
            {
                return NotFound();
            }

            db.Update(url);
            db.SaveChanges();
            return Ok(url);
        }

        // DELETE api/feeds/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            FeedList url = db.UrlsList.FirstOrDefault(x => x.Id == id);
            if (url == null)
            {
                return NotFound();
            }
            db.UrlsList.Remove(url);
            db.SaveChanges();
            return Ok(url);
        }
    }
}
