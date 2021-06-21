using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma_Project.Entity_lib.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AddressOfPoint { get; set; }
        public string AddressOfOwner{ get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string RSch { get; set; }
        public string Owner { get; set; }
        public  List<Seller> Sellers { get; set; } = new List<Seller>();
    }
}
