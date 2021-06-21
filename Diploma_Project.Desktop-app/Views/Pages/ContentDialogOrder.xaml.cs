using Diploma_Project.Desktop_app.Utils;
using Diploma_Project.Desktop_app.Views.UserControls;
using Diploma_Project.Entity_lib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ContentDialogOrder
    {

        public UserControlOrders userControlOrders { get; set; }

        public ContentDialogOrder()
        {
            InitializeComponent();
        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            loadOrder();
        }

        void loadOrder()
        {
            listProducts.ItemsSource = null;
            List<OrderProduct> collection = AppData.currenOrder.OrderProducts;
            listProducts.ItemsSource = collection;
            if (collection.Count()==0)
            {
                IsPrimaryButtonEnabled = false;
            }
            else
            {
                IsPrimaryButtonEnabled = true;
            }

        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            try
            {
                
                if (listProducts.Items.Count! > 0)
                {
                    if (AppData.currentUser.GetType() == typeof(Seller))
                    {

                        foreach (OrderProduct item in listProducts.Items)
                        {
                            if (item.Amount > item.Product.TotalAmount)
                                throw new Exception($"Количество {item.Amount} товара {item.Product.Name} отсутствует на складе");
                        }

                            using (EntitiesContext entitiesContext = new EntitiesContext())
                            {
                                Order order = new Order()
                                {
                                    OrderProducts = AppData.currenOrder.OrderProducts,
                                    StartDateOrder = DateTime.Now,
                                    PointId = ((Seller)AppData.currentUser).PointId,
                                    StatusId = 1,
                                    UserId = AppData.currentUser.Id,
                                };
                                foreach (var item in order.OrderProducts)
                                {
                                    using (EntitiesContext entitiesContext2 = new EntitiesContext())
                                    {
                                        Operation operation = new Operation();
                                        operation.Amount = item.Amount;
                                        operation.Product = item.Product;
                                        operation.User = AppData.currentUser;
                                        operation.DateOperation = DateTime.Now;
                                        operation.TypeOperationId = 4;
                                        entitiesContext2.Attach(operation);
                                        entitiesContext2.Add(operation);
                                        entitiesContext2.Attach(item.Product);
                                        item.Product.writeOff(item.Amount);
                                        entitiesContext2.Update(item.Product);
                                        entitiesContext2.SaveChanges();
                                    }
                                }
                                entitiesContext.Attach(order);
                                entitiesContext.Add(order);
                                entitiesContext.SaveChanges();
                                MessageBox.Show($"Номер вашего заказа {AppData.DB.Orders.OrderBy(o => o.Id).Last().Id}");
                                AppData.currenOrder.OrderProducts.Clear();
                            }

                        
                    }
                    else throw new Exception("Только продавцы могут совершать заказы");
                }
                else throw new Exception("Заказ пустой");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,MessageBoxImage.Error);
            }
            

        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        private void ContentDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            userControlOrders.defaultSearch();
        }
        private void btnDel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var curItem = (OrderProduct)((ListBoxItem)listProducts.ContainerFromElement((TextBlock)sender)).Content;
            MessageBoxResult messageBox = MessageBox.Show($"Удалить из заказа {curItem.Product.Name}?", "Удаленние", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBox == MessageBoxResult.Yes)
            {              
                try
                {                   
                    if (curItem != null)
                    {
                        AppData.currenOrder.OrderProducts.Remove(curItem);                      
                        loadOrder();
                    }
                    else
                    {
                        throw new Exception("Ошибка удаления");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
