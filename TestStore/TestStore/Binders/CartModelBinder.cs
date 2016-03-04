using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestStore.Models;
using System.Web.Mvc;
namespace TestStore.Binders
{
    public class CartModelBinder:IModelBinder
    {
        private const string sessionKey = "CartInfo";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            CartInfo cart = (CartInfo)controllerContext.HttpContext.Session[sessionKey];
            if(cart==null)
            {
                cart = new CartInfo();
                controllerContext.HttpContext.Session[sessionKey] = cart;
            }
            return cart;


        }
    }
}