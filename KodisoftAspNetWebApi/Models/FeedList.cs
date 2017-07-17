using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KodisoftAspNetWebApi.Models
{
    public class FeedList
    {

        public int Id { get; set; }

        public string Group { get; set; }

        public string SubGroup { get; set; }

        [Required(ErrorMessage = "Укажите URL ресурса")]
        public string FeedUrl { get; set; }

        public List<FeedItem> FeedItems { get; set; }
    }
}
