using Diploma_Project.Entity_lib.Models;
using Diploma_Project.Web_app.ViewModels;
using Diploma_Project.Web_app.ViewModels.ShopCart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Diploma_Project.Web_app.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly EntitiesContext db;

        public ShopCartController(EntitiesContext context)
        {
            db = context;
        }
        // GET: ShopCartController
        public async Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                Client client = await db.Clients
                    .Include(c=>c.ShopCarts)
                    .ThenInclude(cp => cp.ShopCartProducts)
                    .ThenInclude(scp=>scp.Product)
                    .FirstOrDefaultAsync(c => c.Email == User.Identity.Name);
                if (client?.ShopCarts != null)
                {
                    return View(new IndexViewModel
                    {
                        ShopCartProducts = client.ShopCarts.Last().ShopCartProducts,
                        TitleOfPage = "Корзина",
                        Stores = new SelectList(db.Stores.ToList(), "Id", "Name", 0)

                    }); ;
                }
                else return RedirectToAction("Index", "Products");
            }
            return RedirectToAction("Index", "Account");
        }
        [HttpPost]
        public async Task<ActionResult> Index(List<ShopCart> shopCarts = null, int? storeId = 0) 
        {
            if (User.Identity.IsAuthenticated)
            {
                Client client = await db.Clients
                    .Include(c => c.ShopCarts)
                    .ThenInclude(cp => cp.ShopCartProducts)
                    .ThenInclude(scp => scp.Product)
                    .FirstOrDefaultAsync(c => c.Email == User.Identity.Name);
                if (client != null)
                {
                    return View(new IndexViewModel
                    {
                        ShopCartProducts = client.ShopCarts.Last().ShopCartProducts
                    });
                }
                else return new BadRequestResult();
            }
            return RedirectToAction("Index", "Account");
        }

    }
}
