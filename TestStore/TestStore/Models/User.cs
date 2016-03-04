using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TestStore.Models;
namespace TestStore.Models
{
    public class User
    {public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
      

    }
}