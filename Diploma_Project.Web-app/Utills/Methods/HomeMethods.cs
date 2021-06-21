using Diploma_Project.Entity_lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_Project.Web_app.Utills.Methods
{
    public static class HomeMethods
    {
        public static void FilterProducts(ref IQueryable<Product> products, int? categoryId, string name)
        {
            if (categoryId != null && categoryId != 0)
            {
                products = products.Where(a => a.ProductCategoryId == categoryId);
            }
            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(a => a.Name.Contains(name));
            }
        }
    }
}
