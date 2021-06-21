using Diploma_Project.Entity_lib.Models;
using Diploma_Project.Web_app.ViewModels.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Diploma_Project.Web_app.Utills.Methods.HomeMethods;

namespace Diploma_Project.Web_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly EntitiesContext db;
        public HomeController(EntitiesContext context)
        {
            db = context;
        }
        // GET: HomeController
        public async Task<IActionResult> Index(int? cantegoryProductId = 0, string name = null, int page = 1, SortState sortList = SortState.NameAsc)
        {
            int pageSize = 3;

            IQueryable<Product> products = db.Products.Include(a => a.ProductCategory);

            FilterProducts(ref products, cantegoryProductId, name);

            //SortProducts(ref enrollees, sortList);

            var count = await products.CountAsync();
            var items = await products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            IndexViewModel viewModel = new IndexViewModel()
            {
                TitleOfPage = "Главная",
                PageViewModel = new PaginationViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortList),
                FilterViewModel = new FilterViewModel(db.ProductCategories.ToList(), cantegoryProductId, name),
                Products = items
            };
            return View(viewModel);
        }
        public async Task<ActionResult> GetImage(int id)
        {
            var product = await db.Products.FirstOrDefaultAsync(p=>p.Id==id);
            var image = product.Photo;
            if (image == null)
            {
                return NotFound();
            }
            return File(image, "image/png");
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
