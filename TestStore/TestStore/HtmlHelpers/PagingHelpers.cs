using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestStore.Models;
using System.Text;
namespace TestStore.HtmlHelpers
{
    //класс формирующий ссылки для страниц внизу, использует InfoForSsilka  в качестве модели
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, InfoForSsilka info, Func<int,string>pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for(int i=1; i<=info.TotalPages;i++)
            {
                TagBuilder tag = new TagBuilder("a");//создает теги HTML
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if(i==info.CurrentPage)
                 tag.AddCssClass("selected"); 
                result.Append(tag.ToString());

            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}