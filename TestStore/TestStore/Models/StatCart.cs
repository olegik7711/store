using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestStore.Models;
using System.Data.Entity;
namespace TestStore.Models
{
    public class StatCart
    {
        public int StatCartId { get; set; }
        public string StatCustomerFIO { get; set; }
        public string StatCustomerAddress { get; set; }
        public string StatCustomerEmail { get; set; }
        public string StatCustomerPhone { get; set; }
        public string StatUserName { get; set; }
        public DateTime? StatDate { get; set; }
        public decimal? StatTotalPrice { get; set; }
       public virtual ICollection<ProductName> ProductNames { get; set; }
       
        
    }

    public class ProductName
    {
        public int ProductNameId { get; set; }
        public string Name { get; set; }
        public int StatCartId { get; set; }
        public virtual StatCart StatCard { get; set; }
    }
}