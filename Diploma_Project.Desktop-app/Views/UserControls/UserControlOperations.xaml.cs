using Diploma_Project.Desktop_app.Utils;
using Diploma_Project.Entity_lib.Models;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для UserControlOperation.xaml
    /// </summary>
    public partial class UserControlOperations : UserControl
    {
        List<Operation> allOperations { get; set; } = new List<Operation>();
        public UserControlOperations()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SearchStandart();
            dpStartDate.DisplayDateStart = new DateTime(2020);
            dpStartDate.DisplayDateEnd = DateTime.Now.AddDays(1);
            dpFinishDate.DisplayDateStart = new DateTime(2020);
            dpFinishDate.DisplayDateEnd = DateTime.Now.AddDays(1);

        }

        private void dpFinishDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchStandart();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchStandart();
        }

        private void dgOperation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        public void SearchStandart()
        {
            SearchOperation(textBoxSearch.Text, dpStartDate.SelectedDate, dpFinishDate.SelectedDate);
        }
        void SearchOperation(string search = "", DateTime? startDate = null,DateTime?  finishDate = null)
        {
            allOperations = AppData.DB.Operations.
                Include(o=>o.Product).
                Include(o=>o.User).
                Include(o=>o.TypeOperation).
                OrderByDescending(o=>o.DateOperation).
                AsNoTracking().
                ToList();

            List<Operation> operations = allOperations;
            if (search.Length > 0)
            {
                operations = operations.Where(o => o.User.FullName.Contains(search)).ToList();
                //orders.AddRange(orders.Where(o => o.User.NumberPhone.Contains(search)).ToList());
            }
            if (startDate!=null&&finishDate!=null)
            {
                operations = operations.Where(o => o.DateOperation>=startDate&&o.DateOperation<=finishDate).ToList();
            }
            dgOperation.ItemsSource = operations;
        }
    }
}
