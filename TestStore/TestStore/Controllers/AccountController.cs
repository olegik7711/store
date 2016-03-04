using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestStore.Models;
namespace TestStore.Controllers
{
    public class AccountController : Controller
    { 
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, CartInfo crt)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (TestStoreContext db = new TestStoreContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
                   
                }
                if (user != null)
                {
                   
                    if (user.Roles == "Admin")
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        crt.CartUser = model.Login;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        //FormsAuthentication.SetAuthCookie(model.Login, true);
                        crt.CartUser = model.Login;
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким именем и паролем не существует!\n" +
                        "Пройдите регистрацию");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model, CartInfo crt)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (TestStoreContext db = new TestStoreContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == model.Login);
                }
                if (user == null)
                {
                    using (TestStoreContext db = new TestStoreContext())
                    {
                        db.Users.Add(new User { Login = model.Login, Password = model.Password, Roles="User" });
                        db.SaveChanges();
                        user = db.Users.Where(u => u.Login == model.Login && u.Password == model.Password).FirstOrDefault();
                        crt.CartUser = model.Login;
                    }
                    if (user != null)
                    {
                        crt.CartUser = model.Login;
                        //FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким именем уже существует!");
                }
            }
            return View(model);
        }


        public ActionResult Logoff(CartInfo cart)
        {
            cart.CartUser = "Гость";
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //public ActionResult GuestLogin(CartInfo cart)
        //{
        //    cart.CartUser = "Гость";
        //    return RedirectToAction("Index", "Home");
        //}


        public ActionResult ResetPassword()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ChangeParol model)
        {
            User user = null;
            using (TestStoreContext db = new TestStoreContext())
            {
                user = db.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == model.OldPassword);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.Users.Add(new User { Login = model.Login, Password = model.NewPassword });
                    db.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Не верно введен лоигин или пароль!");
                    return View(model);
                }
            }
        }
    }
}