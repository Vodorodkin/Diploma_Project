using Diploma_Project.Entity_lib.Models;
using Diploma_Project.Web_app.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_Project.Web_app.Utills.Methods
{
    public static class ProductsMethods
    {
        
            public static void SortProducts(ref IQueryable<Product> products, SortState sortList)
            {
                switch (sortList)
                {
                    case SortState.NameDesc:
                        products = products.OrderByDescending(a => a.Name);
                        break;

                    case SortState.AddressAsc:
                        products = products.OrderBy(a => a.Price);
                        break;

                    case SortState.AddressDesc:
                        products = products.OrderByDescending(a => a.Price);
                        break;


                    default:
                        products = products.OrderBy(a => a.Name);
                        break;
                }
            }

            public static void FilterProducts(ref IQueryable<Product> products, string name, int? categoryId)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    products = products.Where(a => a.Name.Contains(name));
                }
                if (categoryId!=0&&categoryId!=null)
                {
                    products = products.Where(a => a.ProductCategoryId==categoryId);
                }
            }
        
    }
}
