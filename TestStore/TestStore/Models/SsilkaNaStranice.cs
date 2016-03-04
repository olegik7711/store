using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestStore.Models;
using TestStore.HtmlHelpers;
namespace TestStore.Models
{//отображает ссылки на страницах финальный класс
    public class SsilkaNaStranice
    {
        public IEnumerable<Product> Products { get; set; }
        public InfoForSsilka Info { get; set; }
        public string CurrentCategory { get; set; }
    }
}