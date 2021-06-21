using Diploma_Project.Entity_lib.Models;
using Diploma_Project.Web_app.ViewModels.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Diploma_Project.Web_app.Utills.Methods.ProductsMethods;

namespace Diploma_Project.Web_app.Controllers
{
    public class ProductsController : Controller
    {
        // GET: ProductsController1
        private readonly EntitiesContext db;

        public ProductsController(EntitiesContext context)
        {
            db = context;
        }
        // GET: StoresController1
        public async Task<IActionResult> Index(string name = null,int categoryId= 0, int page = 1, SortState sortList = SortState.NameAsc)
        {
            int pageSize = 15;

            IQueryable<Product> products = db.Products.Include(p=>p.ProductCategory);

            FilterProducts(ref products,name,categoryId);

            SortProducts(ref products, sortList);

            var count = await products.CountAsync();
            var items = await products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            string caregoryName = "";
            var caregory = await db.ProductCategories.FindAsync(categoryId);
            if (caregory != null)
                caregoryName = caregory.Name;
            else caregoryName = "Весь каталог";
            Client client = new Client();
            if (User.Identity.IsAuthenticated)          
            {
                 client = await db.Clients.FirstOrDefaultAsync(c => c.Email == User.Identity.Name);
            }
            
                IndexViewModel viewModel = new()
            {
                TitleOfPage = caregoryName,
                PageViewModel = new PaginationViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortList),
                Products = items,
                SelectedProductCategory=caregoryName,
                ShopCartProducts=db.ShopCartProducts.Where(scp=>scp.ShopCart.UserId==client.Id)
            };
            return View(viewModel);
        }
        public async Task<IActionResult> GetImage(int id)
        {
            var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
            var image = product?.Photo;
            if (image == null)
            {
                return NotFound();
            }
            return File(image, "image/png");
        }
        public async Task<IActionResult> AddProductInShopCart(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Client client = await db.Clients.FirstOrDefaultAsync(c => c.Email == User.Identity.Name);
                if (client != null)
                {
                    ShopCart shopCart = db.ShopCarts.FirstOrDefault(sc=>sc.UserId==client.Id);
                    if (shopCart == null)
                    {
                        shopCart = new ShopCart {Client=client};
                        shopCart.ShopCartProducts.Add(new ShopCartProduct { Product = db.Products.Find(id), Amount = 1 });
                        db.Add(shopCart);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index", "Products");
                    }
                    else
                    {
                        shopCart.ShopCartProducts.Add(new ShopCartProduct { Product = db.Products.Find(id), Amount = 1 });
                        db.Update(shopCart);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index", "Products");
                    }
                }
                else return new BadRequestResult();
            }
            return RedirectToAction("Index", "Account");
        }

        // GET: ProductsController1/Details/5
        public async Task<IActionResult> Details(int id)
        {
            await db.ProductCategories.LoadAsync();
            var product = await db.Products.FindAsync(id);
            Client client = new Client();
            if (User.Identity.IsAuthenticated)
            {
                client = await db.Clients.FirstOrDefaultAsync(c => c.Email == User.Identity.Name);
            }
            DetailsViewModel viewModel = new()
            {
                TitleOfPage = product.Name,
                Product = product,
                ShopCartProducts = db.ShopCartProducts.Where(scp => scp.ShopCart.UserId == client.Id)
            };
            return View(viewModel);
        }

        // GET: ProductsController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController1/Create
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

        // GET: ProductsController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductsController1/Edit/5
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

        // GET: ProductsController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController1/Delete/5
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
