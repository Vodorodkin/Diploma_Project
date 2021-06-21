using Diploma_Project.Entity_lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_Project.Web_app.ViewModels.Home
{
    public class FilterViewModel
    {
        public FilterViewModel(List<ProductCategory> productCategories, int? productCategoryId, string name)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            CategoryPoducts = new SelectList(productCategories, "Id", "Short", productCategoryId);
            ListCategoryPoducts = productCategories;
            SelectedCategoryPoduct = productCategoryId;
            SelectedName = name;
        }
        public List<ProductCategory> ListCategoryPoducts { get; private set; }
        public SelectList CategoryPoducts { get; private set; }
        public int? SelectedCategoryPoduct { get; private set; }
        public string SelectedName { get; private set; }
    }
}
