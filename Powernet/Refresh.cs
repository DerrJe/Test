using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Powernet.DataBase;

namespace Powernet
{
    public class Refresh : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string value)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(value));
        }

        private List<Users> user;
        private List<Tarif> tarif;

        public List<Users> User { get { return user; } set { user = value; NotifyPropertyChanged("User"); } }
        public List<Tarif> Tarif { get { return tarif; } set { tarif = value; NotifyPropertyChanged("Tarif"); } }
    }
}
