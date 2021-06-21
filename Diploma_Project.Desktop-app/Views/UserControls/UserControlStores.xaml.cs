using Diploma_Project.Desktop_app.Utils;
using Diploma_Project.Desktop_app.Views.Pages;
using Diploma_Project.Entity_lib.Models;
using Microsoft.EntityFrameworkCore;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
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

namespace Diploma_Project.Desktop_app.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UserControOrders.xaml
    /// </summary>
    public partial class UserControlStores : UserControl
    {
        private readonly ContentDialogStore _dialog = new ContentDialogStore();
        public Store currentStore { get; set; }
        List<Store> allStores { get; set; } = new List<Store>();
        public UserControlStores()
        {
            InitializeComponent();
            _dialog.UserControlStores = this;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SearchStandart();
        }

        private void dgStores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgStores.SelectedItem != null)
            {
                currentStore = (Store)dgStores.SelectedItem;
                btnRemove.Visibility = Visibility.Visible;
                btnEdit.Visibility = Visibility.Visible;
            }
            else
            {
                btnRemove.Visibility = Visibility.Collapsed;
                btnEdit.Visibility = Visibility.Collapsed;
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            _dialog.Title = ((Button)sender).Content;
            _dialog.ShowAsync(ContentDialogPlacement.Popup);
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (currentStore != null)
            {
                _dialog.Title = ((Button)sender).Content;
                _dialog.currentStore = currentStore;
                _dialog.ShowAsync(ContentDialogPlacement.Popup);
            }
        }
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (currentStore != null)
            {
                MessageBoxResult messageBox = MessageBox.Show($"Удалить  {currentStore.Name} {currentStore.AddressOfPoint}?", "Удаленние", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBox == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (EntitiesContext entitiesContext = new EntitiesContext())
                        {
                            entitiesContext.Attach(currentStore);
                            entitiesContext.Remove(currentStore);
                            entitiesContext.SaveChanges();
                        }
                        MessageBox.Show($"{currentStore.Name} {currentStore.AddressOfPoint} удален", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        SearchStandart();

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка удаления");
                    }
                }
            }
        }
        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchStandart();
        }
        public void SearchStandart()
        {
            SearchStores(textBoxSearch.Text);
        }
        void SearchStores(string search = "")
        {
            allStores = AppData.DB.Stores.
                OrderBy(o => o.Name).
                AsNoTracking().
                ToList();

            List<Store> stores = allStores;
            if (search.Length > 0)
            {
                stores = stores.Where(o => o.Name.Contains(search) || o.AddressOfPoint.Contains(search)).ToList();
            }
            dgStores.ItemsSource = stores;
        }
    }
}
