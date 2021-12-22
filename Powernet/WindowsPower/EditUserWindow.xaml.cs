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
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public AdministratorWindow window;
        private Users current = new Users();
        public EditUserWindow(Users user)
        {
            InitializeComponent();

            if (user != null)
            {
                current = user;
                if(current.TarifId != null)
                    cmb.SelectedIndex = (int)current.TarifId - 1;

                BlockInfoTxt.Text = "Редактирование";
            }
            else
            {
                BlockInfoTxt.Text = "Добавление";
                current.TarifId = cmb.SelectedIndex + 1;
            }
                
            DataContext = current;

            cmb.ItemsSource = PowernetEntities.GetContext().Tarif.ToList();
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(BlockInfoTxt.Text == "Редактирование")
            {
                try
                {
                    PowernetEntities.GetContext().Users.Where(p => p.Id == current.Id).FirstOrDefault().TarifId = cmb.SelectedIndex + 1;
                    PowernetEntities.GetContext().SaveChanges();
                    window.Update();
                    MessageBox.Show("Изменения Сохранены!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    current.TarifId = cmb.SelectedIndex + 1;
                    PowernetEntities.GetContext().Users.Add(current);
                    PowernetEntities.GetContext().SaveChanges();
                    window.Update();
                    MessageBox.Show("Добавлен новый пользователь!");
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
