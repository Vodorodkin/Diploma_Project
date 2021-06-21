using Diploma_Project.Entity_lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_Project.Web_app.ViewModels.Products
{
    public class DetailsViewModel : BaseViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<ShopCartProduct> ShopCartProducts { get; set; }
    }
}
