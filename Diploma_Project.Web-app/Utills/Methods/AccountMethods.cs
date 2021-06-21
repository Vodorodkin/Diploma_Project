using Diploma_Project.Entity_lib.Models;
using Diploma_Project.Web_app.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_Project.Web_app.Utills.Methods
{
    public static class AccountMethods
    {
        public static void SortOrders(ref IQueryable<Order> orders, SortState sortList)
        {
            switch (sortList)
            {
                case SortState.IdAsc:
                    orders = orders.OrderByDescending(a => a.Id);
                    break;

                case SortState.AddressAsc:
                    orders = orders.OrderBy(a => a.Point.AddressOfPoint);
                    break;

                case SortState.AddressDesc:
                    orders = orders.OrderByDescending(a => a.Point.AddressOfPoint);
                    break;

                case SortState.DateAsc:
                    orders = orders.OrderBy(a => a.StartDateOrder);
                    break;

                case SortState.DateDesc:
                    orders = orders.OrderByDescending(a => a.StartDateOrder);
                    break;

                default:
                    orders = orders.OrderBy(a => a.Id);
                    break;
            }
        }

        public static void FilterOrders(ref IQueryable<Order> orders, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                int id;
                if (Int32.TryParse(name,out id)) orders = orders.Where(s=>s.Id==id);
                orders = orders.Where(a => a.Status.Name.Contains(name) || a.Point.AddressOfPoint.Contains(name));
            }
        }
    }
}
