using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TestStore.Controllers;
using TestStore.Models;
using System.Web.Security;
namespace TestStore.Controllers
{
    public class HomeController : Controller
    {
        public TestStoreContext db = new TestStoreContext();
        public int PageSize = 4;
        public ViewResult Index(string category,int page=1)
        {
            SsilkaNaStranice viewModel = new SsilkaNaStranice
            { Products = db.Products.ToList()
            .Where(p=>category==null|| p.ProductCategory==category)
            .OrderBy(p => p.ProductId)
            .Skip((page - 1) * PageSize)
         .Take(PageSize),
                Info = new InfoForSsilka
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category==null ? db.Products.ToList().Count():
                    db.Products.ToList().Where(e=>e.ProductCategory==category).Count()

                },
            CurrentCategory = category};
            return View(viewModel);
        }

        public ActionResult UserBay(string user, CartInfo cart)
        {
            user = cart.CartUser;
            ViewBag.user = user;
            if (user != null)
            {
                var stcarts = from s in db.StatCarts select s;
                stcarts = stcarts.Where(s => s.StatUserName == user);
                return View(stcarts.ToList());
            }
            else
            {
                TempData["message"] = "Вы еще не совершали покупок в нашем магазине! Войдите или зарегистрируйтесь!";
                return RedirectToAction("Index","Home");
            }
        }
    }
}