using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma_Project.Entity_lib.Models
{
    public class ShopCart
    {
        public int Id { get; set; }
        public string ShopCartName { get; set; }
        public int? UserId { get; set; }
        public List<ShopCartProduct> ShopCartProducts { get; set; } = new List<ShopCartProduct>();

        public Client Client { get; set; }
        public decimal sumShopCart
        {
            get
            {
                decimal sum = 0;
                foreach (var item in ShopCartProducts)
                {
                    sum += item.Product.Price * item.Amount;
                }
                return sum;
            }
        }
    }
}
