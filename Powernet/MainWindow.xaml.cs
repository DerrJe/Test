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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Powernet.WindowsPower;
using Powernet.DataBase;

namespace Powernet
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            CheckUser();
        }

        private void CheckUser()
        {
            var search = PowernetEntities.GetContext().Users.Where(p => p.UserLogin == LoginTxt.Text).FirstOrDefault();

            if (search != null)
            {
                if (search.UserLogin == LoginTxt.Text && search.UserPassword == PasswordTxt.Password)
                {
                    switch (search.Role)
                    {
                        case "Администратор":
                            AdministratorWindow.Login = LoginTxt.Text;
                            AdministratorWindow windowAdmin = new AdministratorWindow();
                            windowAdmin.Show();
                            this.Close();
                            break;
                        case "Пользователь":
                            CustomerWindow.Login = LoginTxt.Text;
                            CustomerWindow windowCustomer = new CustomerWindow();
                            windowCustomer.Show();
                            this.Close();
                            break;
                    }
                }

                else
                {
                    LoginTxt.ToolTip = "Неправильный логин или пароль!";
                    PasswordTxt.ToolTip = "Неправильный логин или пароль!";
                    LoginTxt.BorderBrush = Brushes.Red;
                    PasswordTxt.BorderBrush = Brushes.Red;
                }
                    
            }
            else
                MessageBox.Show("Такого пользователя не существует! Зарегистрируйтесь!");
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow window = new RegisterWindow();
            window.Show();
            this.Close();
        }
    }
}
