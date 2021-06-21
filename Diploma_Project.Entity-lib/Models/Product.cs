using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma_Project.Entity_lib.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public int ProductCategoryId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int TotalAmount { get; set; }
        public  ProductCategory ProductCategory { get; set; }
        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public List<ShopCartProduct> ShopCartProducts { get; set; } = new List<ShopCartProduct>();

        public string availabilityStrting { get
            {
                if(TotalAmount>0)
                {
                    return "В корзину";
                }
                else
                {
                    return "Нет в наличии";
                }
            } }
        public bool isOnOrder(List<OrderProduct> orderProducts)
        {
            return orderProducts.Where(op => op.ProductId == this.Id).Count() < 1;
        }
        public bool availabilityBoll
        {
            get
            {
                if (TotalAmount>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void writeOff(int quantity)
        {
            if(quantity<=TotalAmount)
            {
                TotalAmount -= quantity;
            }
        }
    }
}
