using Diploma_Project.Desktop_app.Utils;
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
    public partial class ContentDialogInputStandart
    {
        public UserControlProducts userControlProducts;
        public Product product;
        public ContentDialogInputStandart()
        {
            InitializeComponent();
        }     

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (product!=null)
            {
                using (EntitiesContext entitiesContext = new EntitiesContext())
                {
                    Operation operation = new Operation();
                    operation.Amount = (int)tbAmout.Value;
                    operation.Product = product;
                    operation.User = AppData.currentUser;
                    operation.DateOperation = DateTime.Now;
                    if (Title.ToString()== "Поставка")
                    {
                        if ((int)tbAmout.Value >0)
                        {
                            operation.TypeOperationId = 1;
                            entitiesContext.Attach(product);
                            entitiesContext.Attach(operation);

                            product.TotalAmount += (int)tbAmout.Value;
                            entitiesContext.Update(product);
                            entitiesContext.Add(operation);
                            entitiesContext.SaveChanges();
                        }
                    }
                    else
                    {

                        if ((int)tbAmout.Value > product.TotalAmount) 
                        {
                            MessageBox.Show("Больше текущего значения!");
                        }
                        else
                        {
                            operation.TypeOperationId = 2;
                            entitiesContext.Attach(product);
                            entitiesContext.Attach(operation);

                            product.writeOff((int)tbAmout.Value);
                            entitiesContext.Update(product);
                            entitiesContext.Add(operation);
                            entitiesContext.SaveChanges();
                        }                     
                    }
                }
            }
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
        private void ContentDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            userControlProducts.defaultSearch();           
        }

        private void tbAmout_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            if(tbAmout.Value<1)
            {
                IsPrimaryButtonEnabled = false;
            }
            else
            {
                IsPrimaryButtonEnabled = true;
            }
        }
    }
}
