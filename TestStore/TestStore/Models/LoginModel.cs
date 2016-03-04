using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace TestStore.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name ="Логин")]
        public string Login { get; set; }

        [Required]
        [Display(Name ="Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name ="Логин")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Пароль")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Подтвердить пароль")]
        [Compare("Password", ErrorMessage ="Пароли не совпали")]
        public string ConfirmPassword { get; set; }


    }

    public class ChangeParol
    {
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required]
        [DataType (DataType.Password)]
        [Display(Name ="Старый пароль")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Введите новый пароль")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Подтвердите новый пароль")]
        [Compare("NewPassword", ErrorMessage ="Пароли не совпали")]
        public string ConfirmNewPassword { get; set; }
    }
   


}