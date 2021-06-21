using Diploma_Project.Desktop_app.Utils;
using Diploma_Project.Entity_lib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Diploma_Project.Desktop_app.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для contentDialogProduct.xaml
    /// </summary>
    public partial class ContentDialogOperation
    {
        Product currentProduct { get; set; }
        Product editedProduct { get; set; }
        public ContentDialogOperation()
        {
            InitializeComponent();
        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            if (currentProduct != null)
            {
                Title = $"Редактирование \"{currentProduct.Name}\"";
                editedProduct = AppData.DB.Products.AsNoTracking().FirstOrDefault(p=>p.Id==currentProduct.Id);
                icEditedProduct.Items.Add(editedProduct);
            }
            else
            {
                editedProduct = new Product();
                icEditedProduct.Items.Add(editedProduct);
                editedProduct.Photo = File.ReadAllBytes("Resources/Icons/bread.png");             
                editedProduct.TotalAmount = 0;
                editedProduct.Price = 0;               
            }
        }

        private void imageProduct_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            try
            {
                string fn = "";
            OpenFileDialog myFile = new OpenFileDialog();
            if (myFile.ShowDialog() == true)
                fn = (myFile.FileName);        
                if (File.Exists(fn))
                {
                    editedProduct.Photo = File.ReadAllBytes(fn);

                }
            }
            catch
            {
                MessageBox.Show("Ошибка чтения файла");
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
    }
}
