
using Diploma_Project.Desktop_app.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Diploma_Project.Desktop_app.Views.Pages;
using Diploma_Project.Desktop_app.Views.UserControls;
using Diploma_Project.Desktop_app.Views.Windows;
using Diploma_Project.Entity_lib.Models;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using ModernWpf.Controls;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Diploma_Project.Desktop_app.ViewModels
{
    public class ViewModelProduct 
    {
        public List<ProductCategory> productCategories { get; set; } 
        public BitmapImage decodedImage { get; set; } 
        public Product editedProduct { get; set; } 
        public bool IsPrimaryButtonEnabled { get => !(editedProduct?.Photo?.Length == 0 ||
           editedProduct.Name?.Length == 0 ||
           editedProduct.ProductCategory == null ||
           editedProduct.Price < 0 ||
           editedProduct.Description?.Length == 0);
        }               
    }
}
