using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diploma_Project.Entity_lib.Models;

namespace Diploma_Project.Desktop_app.Utils
{
    public static class AppData
    {   
        static public EntitiesContext DB = new EntitiesContext();
        static public User currentUser = DB.Managers.FirstOrDefault();
        static public Order currenOrder = new Order();
    }
}
