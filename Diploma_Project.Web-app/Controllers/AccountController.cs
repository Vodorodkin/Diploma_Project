using Diploma_Project.Entity_lib.Models;
using Diploma_Project.Web_app.ViewModels;
using Diploma_Project.Web_app.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Diploma_Project.Web_app.Utills.Methods.AccountMethods;


namespace Diploma_Project.Web_app.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Client> _userManager;

        private EntitiesContext db;
        public AccountController(EntitiesContext context)
        {
            db = context;

        }
        [Authorize]
        public async Task<IActionResult> Index(string name = null, int page = 1, SortState sortList = SortState.IdAsc)
        {
            if (User.Identity.IsAuthenticated)
            {
                int pageSize = 15;

                Client client = await db.Clients.FirstOrDefaultAsync(c => c.Email == User.Identity.Name);

                IQueryable<Order> orders = db.Orders.Where(o=>o.User==client)
                    .Include(o=>o.Point)
                    .Include(o=>o.Status)
                    .Include(o=>o.OrderProducts).ThenInclude(op=>op.Product);

                FilterOrders(ref orders, name);

                SortOrders(ref orders, sortList);

                var count = await orders.CountAsync();
                var items = await orders.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

                IndexViewModel viewModel = new()
                {
                    TitleOfPage = "Аккаунт",
                    PageViewModel = new PaginationViewModel(count, page, pageSize),
                    SortViewModel = new SortViewModel(sortList),
                    Orders = items,
                    Email = client.Email,
                    FullName = client.FullName
                };
                return View(viewModel);
            }
            else return Login();

        }
        public ActionResult Register()
        {
            return View(new RegisterModel());
        }
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Client client = await db.Clients.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (client != null)
                {
                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Account");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return await Index();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Clients.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.Clients.Add(new Client { Email = model.Email, Password = model.Password,FullName=model.FullName });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Account");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> CancelOrder(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Client client = await db.Clients.FirstOrDefaultAsync(c => c.Email == User.Identity.Name);
                if (client!=null)
                {
                    var order = await db.Orders.Include(o=>o.Status).FirstOrDefaultAsync(o=>o.UserId==client.Id&&o.Id==id);
                    if (order!=null)
                    {
                        order.StatusId = 5;
                        db.Update(order);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index", "Account");
                    }
                    else return new BadRequestResult();
                }
                else return new BadRequestResult();
            }
            else return new BadRequestResult();
        }
    }
}
