using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma_Project.Entity_lib.Models
{
     public class Operation
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
        public DateTime DateOperation { get; set; }
        public int TypeOperationId { get; set; }
        public  Product Product { get; set; }
        public  User User { get; set; }
        public TypeOperation TypeOperation { get; set; }
    }
}
