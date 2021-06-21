using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma_Project.Entity_lib.Models
{
    [Table("Clients")]
    public class Client : User
    {
        public string Email { get; set; }

        public  List<ShopCart> ShopCarts { get; set; } = new List<ShopCart>();
    }
}
