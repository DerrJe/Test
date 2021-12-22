using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Powernet.DataBase;

namespace Powernet.WindowsPower
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// У мамы вадима вырос ПЕНСИЛ
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            if(LoginTxt.Text != "")
            {
                if (LoginTxt.BorderBrush != Brushes.Red)
                {
                    if (PasswordTxt.BorderBrush != Brushes.Red)
                    {
                        if (RepeatPassword.BorderBrush != Brushes.Red)
                        {
                            if (CheckUser() == false)
                            {
                                AddUserInTable();
                            }
                            else
                            {
                                MessageBox.Show($"Пользователь {LoginTxt.Text} уже существует!");
                            }
                        }
                    }
                }
            }

            else
            {
                MessageBox.Show("Избавьтесь от ошибок!");
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        //sex
        private void CheckErrors()
        {
            StringBuilder login = new StringBuilder();
            StringBuilder password = new StringBuilder();
            StringBuilder repeatPassword = new StringBuilder();
            
            if(LoginTxt.Text.Length < 6)
                login.AppendLine("Логин меньше 6 символов!");

            if (string.IsNullOrWhiteSpace(LoginTxt.Text))
                login.AppendLine("Укажите логин!");

            if (!PasswordTxt.Password.Intersect("!@#$%^&*.").Any())
                password.AppendLine("Пароль должен содержать символы (!@#$%^&*.)");

            if (!PasswordTxt.Password.Any(char.IsDigit))
                password.AppendLine("Пароль должен содержать хоть одну цифру!");

            if (!Regex.IsMatch(PasswordTxt.Password, @"[A-Z]"))
                password.AppendLine("Пароль должен содержать одну прописную букву!");

            if (PasswordTxt.Password != RepeatPassword.Password)
                repeatPassword.AppendLine("Пароли не совпадают!");

            if(login.Length > 0)
            {
                LoginTxt.ToolTip = login;
                LoginTxt.BorderBrush = Brushes.Red;
            }

            else
            {
                LoginTxt.ToolTip = null;
                LoginTxt.BorderBrush = Brushes.Transparent;
            }

            if (password.Length > 0)
            {
                PasswordTxt.ToolTip = password;
                PasswordTxt.BorderBrush = Brushes.Red;
            }

            else
            {
                PasswordTxt.ToolTip = null;
                PasswordTxt.BorderBrush = Brushes.Transparent;
            }

            if (repeatPassword.Length > 0)
            {
                RepeatPassword.ToolTip = repeatPassword;
                RepeatPassword.BorderBrush = Brushes.Red;
            }

            else
            {
                RepeatPassword.ToolTip = null;
                RepeatPassword.BorderBrush = Brushes.Transparent;
            }

        }

        private bool CheckUser()
        {
            bool check = false;
            var search = PowernetEntities.GetContext().Users.Where(p => p.UserLogin == LoginTxt.Text).FirstOrDefault();

            if(search != null)
            {
                check = true;
            }

            return check;
        }

        private void AddUserInTable()
        {
            using (PowernetEntities bd = new PowernetEntities())
            {
                Users user = new Users();
                user.UserLogin = LoginTxt.Text;
                user.UserPassword = PasswordTxt.Password;
                user.Role = "Пользователь";
                user.Balance = 0;

                bd.Users.Add(user);
                bd.SaveChanges();
                MessageBox.Show("Регистрация прошла успешно!");

                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            }
        }

        private void LoginTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckErrors();
        }

        private void PasswordTxt_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckErrors();
        }

        private void RepeatPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckErrors();
        }
    }
}
