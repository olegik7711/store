using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestStore.Models
{//модель для добавления ссылок на страницы внизу
    public class InfoForSsilka
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); } }
    }

}