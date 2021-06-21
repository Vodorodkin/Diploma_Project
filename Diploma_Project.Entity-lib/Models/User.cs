using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Diploma_Project.Entity_lib.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NumberPhone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public  List<Order> Orders { get; set; } = new List<Order>();
    }
}