using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestStore.Models;
using System.Data.Entity;
namespace TestStore.Controllers
{
    public class CartController : Controller
    {
        public TestStoreContext db = new TestStoreContext();
        public ViewResult Index(CartInfo cart,string returnUrl)
        { 
            return View(new CartIndexViewModel {Cart =cart, ReturnUrl=returnUrl });
        }

        public RedirectToRouteResult AddToCart(CartInfo cart, int productId, string returnUrl)
        {
            Product product = db.Products.Find(productId);
           
            if(product!=null)
            {
                cart.AddItem(product, 1);
            
            }
            return RedirectToAction("Index", new { returnUrl });

        }


        public RedirectToRouteResult RemoveItemFromCart(CartInfo cart, int productId, string returnUrl)
        {
            Product product = db.Products.Find(productId);

            if (product != null)
            {
                cart.RemoveItem(product, 1);

            }
            return RedirectToAction("Index", new { returnUrl });

        }

        public RedirectToRouteResult RemoveFromCart(CartInfo cart,int productId, string returnUrl)
        {
            Product product = db.Products.Find(productId);
            if(product!=null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Summary(CartInfo cart)
        {
            return View(cart);
        }
         
        public ViewResult Checkout(string user, CartInfo cartinfo)
        {
            user = cartinfo.CartUser;

            if (user != null)
            {
                StatCart stcart = db.StatCarts.FirstOrDefault(u => u.StatUserName == user);
                if (stcart != null)
                {
                    Cart cart = new Cart();
                    cart.CartLogin = user;
                    cart.CustomerAddress = stcart.StatCustomerAddress;
                    cart.CustomerEmail = stcart.StatCustomerEmail;
                    cart.CustomerPhone = stcart.StatCustomerPhone;
                    cart.CustomerFIO = stcart.StatCustomerFIO;
                   return View("Checkout", cart);
                }
                else
                { return View("Checkout", new Cart()); }
            }
            else
            { return View("Checkout", new Cart()); }
        }

        [HttpPost]
        public ActionResult Checkout(Cart newCart, CartInfo cart)
        {
            if (ModelState.IsValid)
            {
                newCart.Date = DateTime.Now;
                newCart.CartLogin = cart.CartUser;
                decimal? totalPrice = 0;
                CartProduct cartProduct = new CartProduct();
                db.Carts.Add(newCart);
                db.SaveChanges();
                foreach (var p in cart.Lines)
                {
                    cartProduct.CartId = newCart.CartId;
                    cartProduct.ProductName = p.Product.ProductName;
                    cartProduct.ProductQuantity = p.Quanttity;
                    cartProduct.SubPrice = (decimal)p.Quanttity * p.Product.ProductPrice;
                    totalPrice += cartProduct.SubPrice;
                    db.CartProducts.Add(cartProduct);
                    db.SaveChanges();
                }
                newCart.TotalPrice = totalPrice;
                db.Entry(newCart).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Спасибо за покупку! Ваши данные отправлены на обработку, наш менеджер вскоре свяжется с Вами!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Checkout", newCart);
            }
        }





    }
}