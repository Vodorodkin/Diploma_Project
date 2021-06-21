using Diploma_Project.Desktop_app.Utils;
using Diploma_Project.Desktop_app.ViewModels;
using Diploma_Project.Desktop_app.Views.UserControls;
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
    public partial class ContentDialogProduct
    {
        public UserControlProducts userControlProducts;
        public List<ProductCategory> productCategories { get; set; } = new List<ProductCategory>();
        public BitmapImage decodedImage { get; set; } = new BitmapImage();

        public Product currentProduct { get; set; }

        public Product editedProduct { get; set; }
        public ContentDialogProduct()
        {
            InitializeComponent();
        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                productCategories = AppData.DB.ProductCategories.Include(pc => pc.Products).ToList();
                DataContext = new ViewModelProduct()
                {
                    productCategories = productCategories,
                };
                if (currentProduct != null)
                {
                    editedProduct = AppData.DB.Products.Include(p => p.ProductCategory).FirstOrDefault(p => p.Id == currentProduct.Id);
                    if (editedProduct.Photo.Length!=0)
                    DecodePhoto(editedProduct.Photo);
                }
                else
                {
                    editedProduct = new Product();
                    editedProduct.Photo = File.ReadAllBytes("Resources/Icons/bread.png");
                    DecodePhoto(editedProduct.Photo);
                    editedProduct.TotalAmount = 0;
                    editedProduct.Price = 0;
                    editedProduct.Description = "";
                    editedProduct.Name = "";
                }
                cbCategory.ItemsSource = productCategories;

                DataContext = new ViewModelProduct()
                {
                    productCategories = productCategories,
                    editedProduct = editedProduct,
                    decodedImage = decodedImage,
                };
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public void DecodePhoto(byte[] byteVal)
        {
            MemoryStream strmImg = new MemoryStream(byteVal);
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.StreamSource = strmImg;
            myBitmapImage.DecodePixelWidth = 200;
            myBitmapImage.EndInit();
            decodedImage = myBitmapImage;
            ((ViewModelProduct)DataContext).decodedImage = decodedImage;
            imageProduct.Source = decodedImage;
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
                DecodePhoto(editedProduct.Photo);
            }
            catch
            {
                MessageBox.Show("Ошибка чтения файла");
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            try
            {             
                    if (IsPrimaryButtonEnabled)
                    {
                        AppData.DB.Attach(editedProduct);
                        if (currentProduct == null)
                        {
                        AppData.DB.Products.Add(editedProduct);
                        }
                        else
                        {
                        AppData.DB.Update(editedProduct);
                        }
                    AppData.DB.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Заполните все поля");
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        private void tbDescriptions_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check)
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }

        private void tbPrice_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            if (check)
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (check)
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }
        bool check { 
            get => (cbCategory.SelectedItem != null && tbPrice.Value > 0 && tbDescriptions.Text.Length > 0 && tbName.Text.Length > 0);
            }

        private void ContentDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            userControlProducts.defaultSearch();
            editedProduct = null;
            currentProduct = null;
            DataContext = null;
        }
    }
}
