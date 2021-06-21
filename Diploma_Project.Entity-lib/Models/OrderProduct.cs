using System;
using System.Collections.Generic;
using System.Text;

namespace Diploma_Project.Entity_lib.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }

        public decimal cost { get
            {
                return Product.Price * Amount;
            } }
    }
}
