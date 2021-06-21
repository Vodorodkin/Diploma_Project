using Diploma_Project.Desktop_app.Utils;
using Diploma_Project.Desktop_app.Views.UserControls;
using Diploma_Project.Entity_lib.Models;
using Microsoft.EntityFrameworkCore;
using Diploma_Project.Desktop_app.ViewModels;
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
    public partial class ContentDialogSeller
    {
        public Seller currentSeller { get; set; }
        Seller editedSeller { get; set; }

        public UserControlSellers userControlSellers;
        List<Store> allStores { get; set; } = new List<Store>();


        public ContentDialogSeller()
        {
            InitializeComponent();
        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            allStores = AppData.DB.Stores.ToList();
            if (currentSeller != null)
            {
                editedSeller = AppData.DB.Sellers.Find(currentSeller.Id);
                pbPassword.Password = editedSeller.Password;
                pbRepeatPassword.Password = editedSeller.Password;
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                pbPassword.Password = "";
                pbRepeatPassword.Password = "";
                editedSeller = new Seller();
                IsPrimaryButtonEnabled = false;
            }
            DataContext = new ViewModelSeller()
            {
                Stores = allStores,
                editedSeller = editedSeller,
            };

        }


        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            try
            {
                using (EntitiesContext entitiesContext = new EntitiesContext())
                {

                    if (IsPrimaryButtonEnabled)
                    {
                        editedSeller.Password = pbPassword.Password;
                        entitiesContext.Attach(editedSeller);
                        if (currentSeller == null)
                        {
                            entitiesContext.Sellers.Add(editedSeller);
                        }
                        else
                        {
                            entitiesContext.Update(editedSeller);
                        }
                        entitiesContext.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Заполните все поля");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения");
            }
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
        private void ContentDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            userControlSellers.SearchStandart();
            editedSeller = null;
            currentSeller = null;
            allStores = null;
            DataContext = null;
        }

        private void ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
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

        private void TextChanged(object sender, TextChangedEventArgs e)
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

        private void pbPassword_PasswordChanged(object sender, RoutedEventArgs e)
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

        private void cbPoints_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        bool check
        {
            get => (tbFullName.Text.Length > 0 && cbPoints.SelectedItem != null
                && tbLogin.Text.Length > 0
                && pbPassword.Password.Length > 0 && pbRepeatPassword.Password.Length > 0
                && pbPassword.Password == pbRepeatPassword.Password
                && tbPhone.Value.ToString().Length == 10);
        }
    }
}
