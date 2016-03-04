using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace TestStore.Models
{
    public class Product
    {
        [HiddenInput(DisplayValue =false)]
        public int ProductId { get; set; }
        [DisplayName("Категория")]
        public string ProductCategory { get; set; }
        [DisplayName("Название")]
        public string ProductName { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Описание продукта")]
        public string ProductDescription { get; set; }
        [DisplayName("Колличество товара")]
        public int ProductQuantity { get; set; }
        [DisplayName("Цена")]
        public decimal ProductPrice { get; set; }

        [HiddenInput(DisplayValue =false)]
        public string ImageType { get; set; }
        public byte[] ImageData { get; set; }
    }
}