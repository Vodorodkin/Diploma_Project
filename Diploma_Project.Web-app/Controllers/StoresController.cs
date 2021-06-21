using Diploma_Project.Entity_lib.Models;
using Diploma_Project.Web_app.ViewModels.Strores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Diploma_Project.Web_app.Utills.Methods.StoresMethods;

namespace Diploma_Project.Web_app.Controllers
{
    public class StoresController : Controller
    {
        private readonly EntitiesContext db;

        public StoresController(EntitiesContext context)
        {
            db = context;
        }
        // GET: StoresController1
        public async Task<IActionResult> Index(string name = null, int page = 1, SortState sortList = SortState.NameAsc)
        {
            int pageSize = 15;

            IQueryable<Store> stores = db.Stores;

            FilterStores(ref stores, name);

            SortStores(ref stores, sortList);

            var count = await stores.CountAsync();
            var items = await stores.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            IndexViewModel viewModel = new()
            {
                TitleOfPage = "Магазины",
                PageViewModel = new PaginationViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortList),
                Stores = items,
                SelectedName = name
            };
            return View(viewModel);
        }   
    }
}
