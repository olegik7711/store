using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestStore.Models;
using System.Data.Entity;
namespace TestStore.Models
{
    public class CartProduct
    {
        public int CartProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal? SubPrice { get; set; }
        public int CartId { get; set; }
        public virtual Cart Carti { get; set; }
    }
}