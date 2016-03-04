using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestStore.Models
{
    public class CartIndexViewModel
    {
        public CartInfo Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}