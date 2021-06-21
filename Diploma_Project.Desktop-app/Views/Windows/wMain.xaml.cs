using Diploma_Project.Desktop_app.Utils;
using Diploma_Project.Entity_lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Diploma_Project.Desktop_app.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для wMain.xaml
    /// </summary>
    public partial class wMain : Window
    {
        public wMain()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (AppData.currentUser!=null)
            {
                if (AppData.currentUser.GetType()!=typeof(Manager))
                {
                    tabItemEmployees.Visibility = Visibility.Collapsed;
                    tabItemOperations.Visibility = Visibility.Collapsed;
                    tabItemPoints.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new wStart().Show();
            AppData.currenOrder = new Order();
            AppData.DB=new EntitiesContext();
        }
    }
}
