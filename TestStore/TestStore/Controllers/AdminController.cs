using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TestStore.Models;
using System.Web.Security;

namespace TestStore.Controllers
{[Authorize]
    public class AdminController : Controller
    {
        public TestStoreContext db = new TestStoreContext();

        public ActionResult Index(string sort, string search)
        {
            ViewBag.Pokupatel = db.Carts.Count();
            ViewBag.CategorySortParm = String.IsNullOrEmpty(sort) ? "ProductCategory desc" : "";
            ViewBag.PriceSortParm = sort == "ProductPrice" ? "ProductPrice desc" : "ProductPrice";
            ViewBag.NameSortParm = sort == "ProductName" ? "ProductName desc" : "ProductName";
            ViewBag.QuantityParm = sort == "ProductQuantity" ? "ProductQuantity desc" : "ProductQuantity";


            var stats = from s in db.Products select s;
            if (!String.IsNullOrEmpty(search))
            {
                stats = stats.Where(s => s.ProductDescription.ToUpper().Contains(search.ToUpper())
                 || s.ProductName.Contains(search.ToUpper()) || s.ProductCategory.Contains(search.ToUpper())
                 || s.ProductId.ToString().Contains(search.ToUpper()) || s.ProductQuantity.ToString().Contains(search)
                 || s.ProductPrice.ToString().Contains(search));

            }
          
            switch (sort)
            {
                case "ProductCategory desc":
                    stats = stats.OrderByDescending(s => s.ProductCategory);
                    break;
                case "ProductQuantity":
                    stats = stats.OrderBy(s => s.ProductQuantity);
                    break;
                case "ProductQuantity desc":
                    stats = stats.OrderByDescending(s => s.ProductQuantity);
                    break;
                case "ProductName desc":
                    stats = stats.OrderByDescending(s => s.ProductName);
                    break;
                case "ProductName":
                    stats = stats.OrderBy(s => s.ProductName);
                    break;
                case "ProductPrice":
                    stats = stats.OrderBy(s => s.ProductPrice);
                    break;
                case "ProductPrice desc":
                    stats = stats.OrderByDescending(s => s.ProductPrice);
                    break;
                default:
                    stats = stats.OrderBy(s => s.ProductCategory);
                    break;
            }

           
            return View(stats.ToList());
        }

        public ActionResult Details(int id)
        {
            try
            {
                Product product = db.Products.Find(id);
          
                return View(product);
            }
            catch
            {
                ModelState.AddModelError("", "Не существует товара");
                return View("Index");
            }
        }

        public ActionResult Create()
        {
            Product newProduct = new Product();
            return View("Create", newProduct);
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase image)
        {
            if(ModelState.IsValid)
            {
                if(image!=null)
                {
                    product.ImageType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            else
            { return View("Create", product); }
        }

        public FileContentResult GetImage(int id)
        {
            Product product = db.Products.Find(id);
            if(product!=null)
            {
                return File(product.ImageData, product.ImageType);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            if(product!=null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
                
            }
            return RedirectToAction("Index", "Admin");
        }

        public ViewResult Edit(int id)
        {
            Product product = db.Products.Find(id);
           
                return View("Edit",product);
            
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {
            if(ModelState.IsValid)
            {
                if(image!=null)
                {
                    product.ImageType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            else { return View(product); }
        }

        public ActionResult IndexCart()
        {
            return View(db.Carts.ToList());
        }

    }
}