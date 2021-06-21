using Diploma_Project.Entity_lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_Project.Web_app.ViewModels.Products
{
    public class IndexViewModel : BaseViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ShopCartProduct> ShopCartProducts { get; set; }

        public PaginationViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public string SelectedProductCategory { get; set; }
    }
}
