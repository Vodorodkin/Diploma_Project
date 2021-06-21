using Diploma_Project.Entity_lib.Models;
using Diploma_Project.Web_app.ViewModels.Strores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_Project.Web_app.Utills.Methods
{
    public static class StoresMethods
    {
        public static void SortStores(ref IQueryable<Store> stores, SortState sortList)
        {
            switch (sortList)
            {
                case SortState.NameDesc:
                    stores = stores.OrderByDescending(a => a.Name);
                    break;

                case SortState.AddressAsc:
                    stores = stores.OrderBy(a => a.AddressOfPoint);
                    break;

                case SortState.AddressDesc:
                    stores = stores.OrderByDescending(a => a.AddressOfPoint);
                    break;

                case SortState.OwnerAsc:
                    stores = stores.OrderBy(a => a.Owner);
                    break;

                case SortState.OwnerDesc:
                    stores = stores.OrderByDescending(a => a.Owner);
                    break;

                default:
                    stores = stores.OrderBy(a => a.Name);
                    break;
            }
        }

        public static void FilterStores(ref IQueryable<Store> stores, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                stores = stores.Where(a => a.Name.Contains(name)||a.AddressOfPoint.Contains(name));
            }
        }
    }
}
