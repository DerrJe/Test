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
using Powernet.DataBase;

namespace Powernet.WindowsPower
{
    /// <summary>
    /// Логика взаимодействия для TarifsInfoPage.xaml
    /// </summary>
    public partial class TarifsInfoPage : Page
    {
        public TarifsInfoPage()
        {
            InitializeComponent();
            ListTarifs.ItemsSource = PowernetEntities.GetContext().Tarif.ToList();
        }

        private void AddTarifBtn_Click(object sender, RoutedEventArgs e)
        {
            Tarif currentTarif = (sender as Button).DataContext as Tarif;

            var search = PowernetEntities.GetContext().Users.Where(p => p.UserLogin == CustomerWindow.Login).FirstOrDefault();

            if((search.Balance - currentTarif.Price) < 0)
            {
                MessageBox.Show("Пополни баланс дура!");
            }
            else
            {
                search.TarifId = currentTarif.Id;
                search.Balance = search.Balance - currentTarif.Price;

                PowernetEntities.GetContext().SaveChanges();

                MessageBox.Show($"Установлен новый тариф {currentTarif.Title}!\nС вас списали {currentTarif.Price}\nУ вас осталось на балансе {search.Balance}");
            }
        }
    }
}
