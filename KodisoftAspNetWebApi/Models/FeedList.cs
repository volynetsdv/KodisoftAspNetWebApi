using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KodisoftAspNetWebApi.Models
{
    public class FeedList
    {
        //public int Id { get; set; }
        //[Required(ErrorMessage = "Укажите имя пользователя")]
        //public string Name { get; set; }
        //[Range(1, 100, ErrorMessage = "Возраст должен быть в промежутке от 1 до 100")]
        //[Required(ErrorMessage = "Укажите возраст пользователя")]
        //public int Age { get; set; }

        public int Id { get; set; }

        public string Group { get; set; }

        public string SubGroup { get; set; }

        [Required(ErrorMessage = "Укажите URL ресурса")]
        public string FeedUrl { get; set; }

        public List<FeedItem> FeedItems { get; set; }
    }
}
