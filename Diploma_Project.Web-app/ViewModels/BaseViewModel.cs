using Diploma_Project.Entity_lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_Project.Web_app.ViewModels
{
    public class BaseViewModel
    {
        public  BaseViewModel()
            {
            using(EntitiesContext context = new EntitiesContext())
            {
                var items = context.ProductCategories.ToList();
                items.Insert(0,new ProductCategory() { Name = "Весь каталог", Id = 0 });
                ProductCategories = items;
            }
            }
        public string TitleOfPage {get; set; }
        public string SelectedProductName { get; set; }

        public IEnumerable<ProductCategory> ProductCategories  { get; set; }

    }
}
