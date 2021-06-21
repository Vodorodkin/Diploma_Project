using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma_Project.Entity_lib.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public  List<Order> Orders { get; set; } = new List<Order>();
        public string StatusColor
        {
            get
            {
                if (Name == "Новый") return "PaleVioletRed";
                else if(Name == "Принят") return "SkyBlue";
                else if(Name == "Ожидает получателя") return "GreenYellow";
                else if(Name == "Исполнен") return "Green";
                else if(Name == "Отменен") return "Gray";
                return "Black";

            }
        }

    }
}
