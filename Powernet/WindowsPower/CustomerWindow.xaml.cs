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
using Powernet.PagesPower;
using Powernet.DataBase;

namespace Powernet.WindowsPower
{
    /// <summary>
    /// Логика взаимодействия для CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public static string Login { get; set; }
        public CustomerWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MenuPage());
            Manager.MyFrame = MainFrame;
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

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MyFrame.GoBack();
        }

        private void UserInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            var search = PowernetEntities.GetContext().Users.Where(p => p.UserLogin == Login).FirstOrDefault();
            Manager.MyFrame.Navigate(new UserInfoPage(search));
        }

        private void TarifsInfo_Click(object sender, RoutedEventArgs e)
        {
            Manager.MyFrame.Navigate(new TarifsInfoPage());
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (Manager.MyFrame.CanGoBack)
                GoBackBtn.IsEnabled = true;
            else
                GoBackBtn.IsEnabled = false;
        }
    }
}
