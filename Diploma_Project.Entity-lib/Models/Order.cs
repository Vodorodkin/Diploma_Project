using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma_Project.Entity_lib.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int? ManagerId { get; set; }
        public int UserId { get; set; }
        public int PointId { get; set; }
        public int StatusId { get; set; }
        public DateTime StartDateOrder { get; set; }
        public DateTime? EndDateOrder { get; set; }
        public bool PayMark { get; set; }
        public  Manager Manager { get; set; }
        public  User User { get; set; }
        public  Store Point { get; set; }
        public  Status Status { get; set; }
        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public decimal sumOrder { get
            {
                decimal sum = 0;
                foreach (var item in OrderProducts)
                {
                    sum += item.Product.Price * item.Amount;
                }
                return sum;
            } }
    }
}
