using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma_Project.Entity_lib.Models
{
    [Table("Sellers")]
    public class Seller : User
    {
        public int PointId { get; set; }
        public  Store Point { get; set; }
    }
}
