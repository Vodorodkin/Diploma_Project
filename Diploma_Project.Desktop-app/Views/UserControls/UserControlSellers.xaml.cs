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
    /// Логика взаимодействия для UserControlSellers.xaml
    /// </summary>
    public partial class UserControlSellers : UserControl
    {
        private readonly ContentDialogSeller _dialog = new ContentDialogSeller();
        public Seller currentSeller { get; set; }
        List<Seller> allSellers { get; set; }
        List<Store> allStores { get; set; } = new List<Store>();

        public UserControlSellers()
        {
            InitializeComponent();
            _dialog.userControlSellers = this;
        }
        public void loadPoints()
        {
            allStores = new List<Store>();
            allStores.Add(new Store { Name = "Все" });
            allStores.AddRange(AppData.DB.Stores.AsNoTracking().ToList());
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadPoints();
            comboBoxStores.ItemsSource = allStores;
            comboBoxStores.SelectedIndex = 0;
        }
        private void dgSellers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSellers.SelectedItem != null)
            {
                currentSeller = (Seller)dgSellers.SelectedItem;
                btnRemove.Visibility = Visibility.Visible;
                btnEdit.Visibility = Visibility.Visible;
            }
            else
            {
                btnRemove.Visibility = Visibility.Collapsed;
                btnEdit.Visibility = Visibility.Collapsed;
            }
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchStandart();
        }

        private void comboBoxStores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchStandart();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            _dialog.Title = ((Button)sender).Content;
            _dialog.ShowAsync(ContentDialogPlacement.Popup);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (currentSeller != null)
            {
                _dialog.Title = ((Button)sender).Content;
                _dialog.currentSeller = currentSeller;
                _dialog.ShowAsync(ContentDialogPlacement.Popup);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (currentSeller != null)
            {
                MessageBoxResult messageBox = MessageBox.Show($"Удалить  {currentSeller.FullName}", "Удаленние", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBox == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (EntitiesContext entitiesContext = new EntitiesContext())
                        {
                            entitiesContext.Attach(currentSeller);
                            entitiesContext.Remove(currentSeller);
                            entitiesContext.SaveChanges();
                        }
                        MessageBox.Show($"{currentSeller.FullName} удален", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        SearchStandart();

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка удаления");
                    }
                }
            }
        }
        public void SearchStandart()
        {
            SearchSellers(textBoxSearch.Text,(Store)comboBoxStores.SelectedItem);
        }
        void SearchSellers(string search = "",Store store = null)
        {
            allSellers = AppData.DB.Sellers.
                Include(s=>s.Point).
                OrderBy(s => s.FullName).
                AsNoTracking().
                ToList();

            List<Seller> sellers = allSellers;
            if (search.Length > 0)
            {
                sellers = sellers.Where(s => s.FullName.Contains(search) || s.NumberPhone.Contains(search)).ToList();
            }
            if (store!=null&&store.Name!="Все")
            {
                sellers = sellers.Where(s => s.Point.Id == store.Id).ToList();
            }
            dgSellers.ItemsSource = sellers;
        }
    }
}
