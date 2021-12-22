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
using Powernet.DataBase;

namespace Powernet.WindowsPower
{
    /// <summary>
    /// Логика взаимодействия для AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        Refresh refresh = new Refresh();
        public static string Login { get; set; }
        public AdministratorWindow()
        {
            InitializeComponent();
            AdminLoginText.Text = Login;
            Update();
            var allTarifs = PowernetEntities.GetContext().Tarif.ToList();

            allTarifs.Insert(0, new Tarif { Title = "Все тарифы" });
            cmbTarifs.ItemsSource = allTarifs;
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

        private void EditUsersBtn_Click(object sender, RoutedEventArgs e)
        {
            EditUserWindow editUserWindow = new EditUserWindow((sender as Button).DataContext as Users);
            editUserWindow.window = this;
            editUserWindow.ShowDialog();
        }

        private void EditTarifsBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
        public void Update()
        {
            DataContext = null;
            refresh.Tarif = PowernetEntities.GetContext().Tarif.ToList();
            refresh.User = PowernetEntities.GetContext().Users.ToList();
            DataContext = refresh;
        }

        private void AddTarifBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteTarifBtn_Click(object sender, RoutedEventArgs e)
        {
            var search = ListTarifs.SelectedItems.Cast<Tarif>().ToList();

            if (search.Count() > 0)
            {
                if (MessageBox.Show($"Вы уверены, что хотите удалить {search.Count} строк(у)", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    PowernetEntities.GetContext().Tarif.RemoveRange(search);
                    PowernetEntities.GetContext().SaveChanges();
                    Update();
                }
            }
            else
                MessageBox.Show("Выберите строки для удаления!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            EditUserWindow editUserWindow = new EditUserWindow((sender as Button).DataContext as Users);
            editUserWindow.window = this;
            editUserWindow.ShowDialog();
        }

        private void DeleteUserBtn_Click(object sender, RoutedEventArgs e)
        {
            var search = ListUsers.SelectedItems.Cast<Users>().ToList();

            if(search.Count() > 0)
            {
                if(MessageBox.Show($"Вы уверены, что хотите удалить {search.Count} строк(у)", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    PowernetEntities.GetContext().Users.RemoveRange(search);
                    PowernetEntities.GetContext().SaveChanges();
                    Update();
                }
            }
            else
                MessageBox.Show("Выберите строки для удаления!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTxt.Text != "")
                ListUsers.ItemsSource = PowernetEntities.GetContext().Users.Where(p => p.UserName.Contains(SearchTxt.Text.ToLower())).ToList();
            else
                ListUsers.ItemsSource = PowernetEntities.GetContext().Users.ToList();
        }

        private void cmbTarifs_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTarifs.SelectedIndex > 0)
            {
                ListUsers.ItemsSource = PowernetEntities.GetContext().Users.Where(p => p.TarifId == cmbTarifs.SelectedIndex).ToList();
            }
            else
            {
                ListUsers.ItemsSource = PowernetEntities.GetContext().Users.ToList();
                SearchTxt.Text = "";
            }
        }

        private void UpdateUsers()
        {
            var list = PowernetEntities.GetContext().Users.ToList();

            if(SearchTxt.Text != "")
            {
                list = list.Where(p => p.UserName.Contains(SearchTxt.Text.ToLower())).ToList();
                if (cmbTarifs.SelectedIndex > 0)
                    list = list.Where(p => p.TarifId == cmbTarifs.SelectedIndex).ToList();
                else
                {
                    list = PowernetEntities.GetContext().Users.ToList();
                    SearchTxt.Text = "";
                }
            }
            else
            {
                if (cmbTarifs.SelectedIndex > 0)
                {
                    list = list.Where(p => p.TarifId == cmbTarifs.SelectedIndex).ToList();

                    if(SearchTxt.Text != "")
                    {
                        list = list.Where(p => p.UserName.Contains(SearchTxt.Text.ToLower())).ToList();
                    }
                }
                else
                {
                    list = PowernetEntities.GetContext().Users.ToList();
                    SearchTxt.Text = "";
                }
            }

            ListUsers.ItemsSource = list;
        }
    }
}
