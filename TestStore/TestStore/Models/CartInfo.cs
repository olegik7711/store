using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestStore.Models;
using System.Data.Entity;
namespace TestStore.Models
{
    public class CartInfo
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public string CartUser { get; set; }
        public void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Product.ProductId == product.ProductId)
                .FirstOrDefault();
            if(line==null)
            {
                lineCollection.Add(new CartLine { Product = product, Quanttity = quantity });
            }
            else
            {
                line.Quanttity += quantity;
            }
        }

        public void RemoveItem(Product product, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Product.ProductId == product.ProductId)
                .FirstOrDefault();
            if (line != null)
            {
                line.Quanttity -= quantity;
            }
           
        }



        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);
        }

        public decimal CoputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.ProductPrice * e.Quanttity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine>Lines
        {
            get { return lineCollection; }
        }

    }

    public class CartLine
    { 
       public Product Product { get; set; }
        public int Quanttity { get; set; }
    }
}