using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestStore.Models;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace TestStore.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        [Required(ErrorMessage ="Введите Имя по которому мы сможем обращаться к вам!")]
        public string CustomerFIO { get; set; }

        [Required(ErrorMessage ="Введите адрес для доставки товара!")]
        public string CustomerAddress { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,6}", ErrorMessage ="Введите корректный Email!")]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage ="Телефон необходим для связи с вами!")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Введите корректный телефонный номер! ")]
        public string CustomerPhone { get; set; }


        public DateTime? Date { get; set; }
      
        public decimal? TotalPrice { get; set; }
     
        public string CartLogin { get; set; }
        public virtual ICollection<CartProduct> CartProductos { get; set; }
        
    }
}