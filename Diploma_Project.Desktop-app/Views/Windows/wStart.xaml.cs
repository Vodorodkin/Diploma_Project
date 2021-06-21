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
using Diploma_Project.Desktop_app.Utils;
using Diploma_Project.Entity_lib.Models;
using Diploma_Project.Entity_lib;
using Microsoft.EntityFrameworkCore;

namespace Diploma_Project.Desktop_app.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для wStart.xaml
    /// </summary>
    public partial class wStart : Window
    {
        public wStart()
        {
            InitializeComponent();
            BusinessLogic.DB = new EntitiesContext();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = AppData.DB.Users.FirstOrDefault(u=>u.Password==pbPassword.Password&& u.Login == tbLogin.Text);
                if (user!=null)
                {
                    AppData.currentUser = user;
                    new wMain().Show();
                    Close();
                }               
                else throw new Exception("Пользователь не найден");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
