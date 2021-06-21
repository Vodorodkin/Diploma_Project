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
    public partial class ContentDialogStore
    {
        public UserControlStores UserControlStores;
        public Store currentStore { get; set; }
        Store editedStore { get; set; }
        public ContentDialogStore()
        {
            InitializeComponent();

        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
           if (currentStore!=null)
            {
                editedStore = AppData.DB.Stores.Find(currentStore.Id);
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                editedStore = new Store();
                IsPrimaryButtonEnabled = false;
            }
            DataContext = new ViewModelStore()
            {
                editedStore = editedStore,
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
                        entitiesContext.Attach(editedStore);
                        if (currentStore == null)
                        {
                            entitiesContext.Stores.Add(editedStore);
                        }
                        else
                        {
                            entitiesContext.Update(editedStore);
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
            UserControlStores.SearchStandart();
            editedStore = null;
            currentStore = null;
            DataContext = null;
        }

        private void tbINN_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
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

        private void tbAddressOwner_TextChanged(object sender, TextChangedEventArgs e)
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
        bool check { get => (tbName.Text.Length > 0 && tbOwner.Text.Length > 0 
                && tbAddressOwner.Text.Length > 0 && tbAddressPoint.Text.Length > 0
                && tbINN.Value.ToString().Length == 10 && tbKPP.Value.ToString().Length == 9);
        }
    }
}
