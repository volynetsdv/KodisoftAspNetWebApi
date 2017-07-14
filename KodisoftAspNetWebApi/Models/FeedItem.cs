using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KodisoftAspNetWebApi.Models
{
    public class FeedItem
    {
        public int Id { get; set; }

        public string ResourceUrl { get; set; }

        public int FeedUrlId { get; set; }

        [Required(ErrorMessage = "Не указано имя канала")]
        public string Title { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Не указано описание новости")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Не указана дата/время обновления новости")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy,hh-mm}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedOn { get; set; }

        public FeedList FeedURLs { get; set; }

        public FeedType FeedType { get; set; }
    }
}
