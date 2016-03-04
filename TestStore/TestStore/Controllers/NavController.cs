using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TestStore.Controllers;
using TestStore.Models;
namespace TestStore.Controllers
{
    public class NavController : Controller
    {
        public TestStoreContext db = new TestStoreContext();
        public ViewResult Menu(string category)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = db.Products.ToList()
                .Select(x => x.ProductCategory)
                .Distinct()
                .OrderBy(x => x);
            return View(categories);

        }
    }
}