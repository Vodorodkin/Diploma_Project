using Diploma_Project.Desktop_app.Utils;
using Diploma_Project.Desktop_app.ViewModels;
using Diploma_Project.Desktop_app.Views.Pages;
using Diploma_Project.Desktop_app.Views.Windows;
using Diploma_Project.Entity_lib.Models;
using Microsoft.EntityFrameworkCore;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic;

namespace Diploma_Project.Desktop_app.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UserControlProducts.xaml
    /// </summary>
    public partial class UserControlProducts : UserControl
    {

        private readonly ContentDialogProduct _dialog = new ContentDialogProduct();
        private readonly ContentDialogInputStandart _dialogStandart = new ContentDialogInputStandart();

        public List<ProductCategory> productCategories { get; set; } = new List<ProductCategory>();
        public Product currentProduct { get; set; }
        public List<Product> allProducts { get; set; } = new List<Product>();
        public UserControlProducts()
        {
            InitializeComponent();
            if (AppData.currentUser.GetType() == typeof(Seller))
                buttonsPanel.Visibility = Visibility.Collapsed;
            else
            buttonsPanel.Visibility = Visibility.Visible;
            _dialog.userControlProducts = this;
            productCategories.Add(new ProductCategory() { Name = "Все" });
            productCategories.AddRange(AppData.DB.ProductCategories.AsNoTracking().ToList());
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            defaultSearch();
            comboBoxProductCategories.ItemsSource = productCategories;
            comboBoxProductCategories.SelectedItem = productCategories.FirstOrDefault();
        }
        private void comboBoxProductCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            defaultSearch();
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            defaultSearch();
        }
        public void defaultSearch ()
        {
            searchProducts(textBoxSearch.Text, (ProductCategory)comboBoxProductCategories.SelectedItem);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            _dialog.Title = ((Button)sender).Content;
            _dialog.ShowAsync(ContentDialogPlacement.Popup);
            defaultSearch();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (currentProduct!=null)
            {
                _dialog.Title = ((Button)sender).Content;
                _dialog.currentProduct = currentProduct;
                _dialog.ShowAsync(ContentDialogPlacement.Popup);               
            }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (currentProduct != null)
            {
                MessageBoxResult messageBox = MessageBox.Show($"Удалить продукт {currentProduct.Name}", "Удаленние", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBox == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (EntitiesContext entitiesContext=new EntitiesContext())
                        {
                            entitiesContext.Attach(currentProduct);
                            entitiesContext.Remove(currentProduct);
                            entitiesContext.SaveChanges();
                        }
                        MessageBox.Show($"Продукт {currentProduct.Name} удален", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        defaultSearch();

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка удаления");
                    }
                }
                
            }
                
            
        }
        void searchProducts(string search ="", ProductCategory productCategory = null)
        {
            allProducts = AppData.DB.Products.
                  OrderBy(p => p.Name).
                  Include(p=>p.ProductCategory).
                  AsNoTracking().
                  ToList();

            List<Product> products = allProducts;
            if (search.Length > 0)
            {
                products = products.Where(p => p.Name.Contains(search)).ToList();
            }
            if (productCategory != null && productCategory.Name != "Все")
            {
                products = products.Where(p => p.ProductCategory.Name == productCategory.Name).ToList();
            }
            listlProducts.ItemsSource = products;
        }

        private void listlProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listlProducts.SelectedItem!=null)
            {
                currentProduct = (Product)listlProducts.SelectedItem;
                BtnEdit.Visibility = Visibility.Visible;
                BtnRemove.Visibility = Visibility.Visible;
                BtnWriteOff.Visibility = Visibility.Visible;
                BtnAddInWerehouse.Visibility = Visibility.Visible;
            }
            else
            {
                BtnEdit.Visibility = Visibility.Collapsed;
                BtnRemove.Visibility = Visibility.Collapsed;
                BtnWriteOff.Visibility = Visibility.Collapsed;
                BtnAddInWerehouse.Visibility = Visibility.Collapsed;

            }
        }



        private void BtnWriteOff_Click(object sender, RoutedEventArgs e)
        {
            
            if (currentProduct != null)
            {
                _dialogStandart.Title = ((Button)sender).Content;
                _dialogStandart.userControlProducts = this;
                _dialogStandart.product = currentProduct;
                _dialogStandart.ShowAsync(ContentDialogPlacement.Popup);
            }
        }

        private void BtnAddInWerehouse_Click(object sender, RoutedEventArgs e)
        {
            if (currentProduct != null)
            {
                _dialogStandart.Title = ((Button)sender).Content;
                _dialogStandart.userControlProducts = this;
                _dialogStandart.product = currentProduct;
                _dialogStandart.ShowAsync(ContentDialogPlacement.Popup);

            }
        }      

        private void mybtn_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                int amount = (int)((dynamic)sender).CommandParameter.Value;
                var curItem = ((ListBoxItem)listlProducts.ContainerFromElement((Button)sender)).Content;
                if (amount > 0 && ((Product)curItem).TotalAmount >= amount)
                {
                    int id = ((Product)curItem).Id;
                    if (curItem != null && AppData.currenOrder.OrderProducts.Where(op=>op.Product.Id==id).Count()==0)
                    {                
                            AppData.currenOrder.OrderProducts.Add(new OrderProduct() {Product=((Product)curItem),Amount=amount});
                            ((Button)sender).Content = "В корзине";
                            ((Button)sender).IsEnabled = false;                       
                    }
                    else
                    {
                        MessageBox.Show("Товар уже в корзине", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("На складе нет необходимого количества", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка добавления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
