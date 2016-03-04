using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestStore.Models;
using System.Data.Entity;
using System.Web.Security;
namespace TestStore.Controllers
{[Authorize]
    public class AdminCartController : Controller
    {
        TestStoreContext db = new TestStoreContext();
        public ActionResult Index()
        {
           
            return View(db.Carts.ToList());
        }

        [HttpPost]
        public ActionResult Delete(int id )
        {
            Cart cart = db.Carts.Find(id);
            StatCart stCart = new StatCart();
            stCart.StatUserName = cart.CartLogin;
            stCart.StatCustomerAddress = cart.CustomerAddress;
            stCart.StatCustomerEmail = cart.CustomerEmail;
            stCart.StatCustomerFIO = cart.CustomerFIO;
            stCart.StatCustomerPhone = cart.CustomerPhone;
            stCart.StatDate = cart.Date;
            stCart.StatTotalPrice = cart.TotalPrice;
            db.StatCarts.Add(stCart);
            db.SaveChanges();
            foreach (var p in cart.CartProductos)
            {
                
                ProductName prName = new ProductName();
                prName.StatCartId = stCart.StatCartId;
                prName.Name = p.ProductName;
               
               
                Product product = db.Products.FirstOrDefault(f=>f.ProductName==p.ProductName);
               
                if (product != null)
                {
                    product.ProductQuantity = product.ProductQuantity - p.ProductQuantity;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
                db.ProductNames.Add(prName);
                db.SaveChanges();

            }
           
                db.Carts.Remove(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            
        }

        public ActionResult Statistic (string sort, string search)
        {
            ViewBag.DateSort = String.IsNullOrEmpty(sort) ? "SortDate desc" : "";
            ViewBag.UserSort = sort == "StatUserName" ? "SortUser desc" : "SortUser";
            ViewBag.PriceSort = sort == "StatTotalPrice" ? "StatTotalPrice desc" : "StatTotalPrice";
            var stats = from s in db.StatCarts select s;
            if(!String.IsNullOrEmpty(search))
            {
                stats = stats.Where(s => s.StatCustomerAddress.ToUpper().Contains(search.ToUpper())
                 || s.StatCustomerEmail.Contains(search.ToUpper()) || s.StatCustomerFIO.Contains(search.ToUpper())
                 || s.StatUserName.Contains(search.ToUpper()) || s.StatTotalPrice.ToString().Contains(search)
                 || s.StatCustomerPhone.ToString().Contains(search));

            }

            switch(sort)
            {
                case "SortDate desc":
                    stats = stats.OrderByDescending(s => s.StatDate);
                    break;
                case "SortUser":
                    stats = stats.OrderBy(s => s.StatUserName);
                    break;
                case "SortUser desc":
                    stats = stats.OrderByDescending(s => s.StatUserName);
                    break;
                case "StatTotalPrice desc":
                    stats = stats.OrderByDescending(s => s.StatTotalPrice);
                    break;
                case "StatTotalPrice":
                    stats = stats.OrderBy(s => s.StatTotalPrice);
                    break;
                default:
                    stats = stats.OrderBy(s => s.StatDate);
                    break;
            }
            return View(stats.ToList());
        }

        [HttpPost]
        public ActionResult StatisticDelete(int id)
        {
            StatCart statCart = db.StatCarts.Find(id);
            if(statCart!=null)
            {
                db.StatCarts.Remove(statCart);
                db.SaveChanges();
                return RedirectToAction("Statistic");
            }
            else
            {
                ModelState.AddModelError("", "Нет такой записи!");
                return View("Statistic");
            }
        }
    }
}