using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TestStore.Models;
namespace TestStore.Models
{
    public class TestStoreContext:DbContext
    {
      
        public DbSet<Product> Products { get; set; }
      
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<StatCart> StatCarts { get; set; }
        public DbSet<ProductName> ProductNames { get; set; }
       
    }
}