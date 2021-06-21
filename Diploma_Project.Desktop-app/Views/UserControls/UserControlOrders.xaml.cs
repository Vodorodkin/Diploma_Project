using Diploma_Project.Desktop_app.Utils;
using Diploma_Project.Desktop_app.Views.Pages;
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

namespace Diploma_Project.Desktop_app.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ucOrders.xaml
    /// </summary>
    public partial class UserControlOrders : UserControl
    {
        private readonly ContentDialogOrder _dialog = new ContentDialogOrder();
        List<Order> allOrders { get; set; } = new List<Order>();
        List<Status> allStatuses { get; set; } = new List<Status>();
        List<Store> allPoints { get; set; } = new List<Store>();

        public UserControlOrders()
        {
            InitializeComponent();
            _dialog.userControlOrders = this;
            allStatuses.Add(new Status { Name = "Все" });
            allStatuses.AddRange(AppData.DB.Statuses.AsNoTracking().ToList());
        }
        public void loadPoints()
        {
            allPoints = new List<Store>();
            allPoints.Add(new Store { Name = "Все" });
            allPoints.AddRange(AppData.DB.Stores.AsNoTracking().ToList());
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (AppData.currentUser.GetType() == typeof(Manager))
                btnNew.Visibility = Visibility.Collapsed;
            else
                btnNew.Visibility = Visibility.Visible;
            loadPoints();
            comboBoxPoints.ItemsSource = allPoints;
            comboBoxPoints.SelectedIndex = 0;
            comboBoxSatatuses.ItemsSource = allStatuses;
            defaultSearch();
        }

        void SearchOrders(string search = "", Store point = null, Status status = null)
        {
            dgOrders.ItemsSource = null;
            if (AppData.currentUser.GetType() == typeof(Manager))
            {
                allOrders = AppData.DB.Orders.Where(o=>o.Manager==AppData.currentUser||o.Manager==null).
                   Include(o => o.User).
                   Include(o => o.Point).
                   Include(o => o.Status).
                   Include(o => o.Manager).
                   Include(o => o.OrderProducts).ThenInclude(op => op.Product).
                   OrderByDescending(o => o.StartDateOrder).
                   AsNoTracking().
                   ToList();
            }
            else
            {
                allOrders = AppData.DB.Orders.Where(o=>o.Point.Id==((Seller)AppData.currentUser).PointId).
                   Include(o => o.User).
                   Include(o => o.Point).
                   Include(o => o.Status).
                   Include(o => o.OrderProducts).ThenInclude(op => op.Product).
                   OrderByDescending(o => o.StartDateOrder).
                   AsNoTracking().
                   ToList();
            }                          
            List<Order> orders = allOrders;
            if (search.Length>0)
            {
                orders = orders.Where(o => o.User.FullName.Contains(search)|| o.User.NumberPhone.Contains(search)||o.Id.ToString().Contains(search)).ToList();
                //orders.AddRange(orders.Where(o => o.User.NumberPhone.Contains(search)).ToList());
            }
            if (point != null && point.Name != "Все")
            {
                orders = orders.Where(o => o.Point == point).ToList();
            }
            if (status != null&&status.Name!="Все")
            {
                orders = orders.Where(o => o.Status.Name == status.Name).ToList();
            }
            dgOrders.ItemsSource = orders;
        }
        public void defaultSearch()
        {
            SearchOrders(textBoxSearch.Text, (Store)comboBoxPoints.SelectedItem, (Status)comboBoxSatatuses.SelectedItem);
        }

        private void comboBoxPoints_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            defaultSearch();
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            defaultSearch();
        }

        private void dgOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgOrders.SelectedItem != null)
            {


                if (AppData.currentUser.GetType() == typeof(Manager))
                {
                    if (((Order)dgOrders.SelectedItem).StatusId == 1)
                    {
                        btnApply.Visibility = Visibility.Visible;
                        btnCancel.Visibility = Visibility.Collapsed;
                        btnWait.Visibility = Visibility.Collapsed;
                        btnComplete.Visibility = Visibility.Collapsed;
                        btnExport.Visibility = Visibility.Collapsed;
                    }
                    else if (((Order)dgOrders.SelectedItem).StatusId == 2)
                    {
                        btnApply.Visibility = Visibility.Collapsed;
                        btnCancel.Visibility = Visibility.Collapsed;
                        btnWait.Visibility = Visibility.Collapsed;
                        btnComplete.Visibility = Visibility.Collapsed;
                        btnExport.Visibility = Visibility.Visible;
                    }
                    else if (((Order)dgOrders.SelectedItem).StatusId == 3)
                    {
                        btnApply.Visibility = Visibility.Collapsed;
                        btnCancel.Visibility = Visibility.Collapsed;
                        btnWait.Visibility = Visibility.Collapsed;
                        btnComplete.Visibility = Visibility.Visible;
                        btnExport.Visibility = Visibility.Collapsed;

                    }
                    else if (((Order)dgOrders.SelectedItem).StatusId == 4)
                    {
                        btnApply.Visibility = Visibility.Collapsed;
                        btnCancel.Visibility = Visibility.Collapsed;
                        btnWait.Visibility = Visibility.Collapsed;
                        btnComplete.Visibility = Visibility.Collapsed;
                        btnExport.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        btnApply.Visibility = Visibility.Collapsed;
                        btnCancel.Visibility = Visibility.Collapsed;
                        btnWait.Visibility = Visibility.Collapsed;
                        btnComplete.Visibility = Visibility.Collapsed;
                        btnExport.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    if (((Order)dgOrders.SelectedItem).StatusId == 1)
                    {
                        btnApply.Visibility = Visibility.Collapsed;
                        btnCancel.Visibility = Visibility.Visible;
                        btnWait.Visibility = Visibility.Collapsed;
                        btnComplete.Visibility = Visibility.Collapsed;
                    }
                    else if (((Order)dgOrders.SelectedItem).StatusId == 2)
                    {
                        btnApply.Visibility = Visibility.Collapsed;
                        btnCancel.Visibility = Visibility.Collapsed;
                        btnWait.Visibility = Visibility.Visible;
                        btnComplete.Visibility = Visibility.Collapsed;
                    }
                    else if (((Order)dgOrders.SelectedItem).StatusId == 3)
                    {
                        btnApply.Visibility = Visibility.Collapsed;
                        btnCancel.Visibility = Visibility.Collapsed;
                        btnWait.Visibility = Visibility.Collapsed;
                        btnComplete.Visibility = Visibility.Visible;
                    }
                    else if (((Order)dgOrders.SelectedItem).StatusId == 4)
                    {
                        btnApply.Visibility = Visibility.Collapsed;
                        btnCancel.Visibility = Visibility.Collapsed;
                        btnWait.Visibility = Visibility.Collapsed;
                        btnComplete.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        btnApply.Visibility = Visibility.Collapsed;
                        btnCancel.Visibility = Visibility.Collapsed;
                        btnWait.Visibility = Visibility.Collapsed;
                        btnComplete.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            defaultSearch();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            _dialog.Title = ((Button)sender).Content;
            _dialog.ShowAsync(ContentDialogPlacement.Popup);
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((Order)dgOrders.SelectedItem).StatusId==1&& AppData.currentUser.GetType() == typeof(Manager))
                {
                    MessageBoxResult messageBox = MessageBox.Show($"Принять заказ №{((Order)dgOrders.SelectedItem).Id}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (messageBox == MessageBoxResult.Yes)
                    {
                        using (EntitiesContext entitiesContext = new EntitiesContext())
                        {
                            ((Order)dgOrders.SelectedItem).Status.Id = 2;
                            ((Order)dgOrders.SelectedItem).Manager = (Manager)AppData.currentUser;
                            entitiesContext.Attach((Order)dgOrders.SelectedItem);
                            entitiesContext.Update((Order)dgOrders.SelectedItem);
                            entitiesContext.SaveChanges();
                            defaultSearch();
                        }
                    }
                }

            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void btnWait_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((Order)dgOrders.SelectedItem).StatusId == 2)
                {
                    MessageBoxResult messageBox = MessageBox.Show($"Перевести статус заказа №{((Order)dgOrders.SelectedItem).Id} \"Поступил\"?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (messageBox == MessageBoxResult.Yes)
                    {
                        using (EntitiesContext entitiesContext = new EntitiesContext())
                        {
                            ((Order)dgOrders.SelectedItem).Status.Id = 3;
                            entitiesContext.Attach((Order)dgOrders.SelectedItem);
                            entitiesContext.Update((Order)dgOrders.SelectedItem);
                            entitiesContext.SaveChanges();
                            defaultSearch();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnComplete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((Order)dgOrders.SelectedItem).StatusId==3)
                {
                    MessageBoxResult messageBox = MessageBox.Show($"Завершить заказ №{((Order)dgOrders.SelectedItem).Id}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (messageBox == MessageBoxResult.Yes)
                    { 
                    }

                        using (EntitiesContext entitiesContext = new EntitiesContext())
                    {
                        ((Order)dgOrders.SelectedItem).Status.Id = 4;
                        ((Order)dgOrders.SelectedItem).EndDateOrder = DateTime.Now;
                        entitiesContext.Attach((Order)dgOrders.SelectedItem);
                        entitiesContext.Update((Order)dgOrders.SelectedItem);
                        entitiesContext.SaveChanges();
                        defaultSearch();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {           
            try
            {
                if (((Order)dgOrders?.SelectedItem)?.StatusId == 1)
                {
                    MessageBoxResult messageBox = MessageBox.Show($"Отменить заказ №{((Order)dgOrders.SelectedItem).Id}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (messageBox == MessageBoxResult.Yes)
                    {
                        using (EntitiesContext entitiesContext = new EntitiesContext())
                        {
                            ((Order)dgOrders.SelectedItem).Status.Id = 5;
                            foreach (var item in ((Order)dgOrders.SelectedItem).OrderProducts)
                            {
                                using (EntitiesContext entitiesContext2 = new EntitiesContext())
                                {
                                    Operation operation = new Operation();
                                    operation.Amount = item.Amount;
                                    operation.Product = item.Product;
                                    operation.User = AppData.currentUser;
                                    operation.DateOperation = DateTime.Now;
                                    operation.TypeOperationId = 3;
                                    entitiesContext2.Attach(operation);
                                    entitiesContext2.Add(operation);
                                    entitiesContext2.Attach(item.Product);
                                    item.Product.TotalAmount += item.Amount;
                                    entitiesContext2.Update(item.Product);
                                    entitiesContext2.SaveChanges();
                                }
                            }
                            entitiesContext.Attach((Order)dgOrders.SelectedItem);
                            entitiesContext.Update((Order)dgOrders.SelectedItem);
                            entitiesContext.SaveChanges();
                            defaultSearch();
                        }
                    }

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if ((Order)dgOrders.SelectedItem!=null)
                    {
                Export.exportPDF((Order)dgOrders.SelectedItem);
            }
        }
    }
}
