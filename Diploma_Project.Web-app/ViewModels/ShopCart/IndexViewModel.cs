using Diploma_Project.Entity_lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_Project.Web_app.ViewModels.ShopCart
{
    public class IndexViewModel : BaseViewModel
    {
        public IEnumerable<ShopCartProduct> ShopCartProducts { get; set; }
        public SelectList Stores { get; set; }

    }
}
