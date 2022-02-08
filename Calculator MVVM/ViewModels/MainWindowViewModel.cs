using Calculator_MVVM.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Calculator_MVVM
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel() { 
            ScreenVal = "0";
            AddNumberCommand = new RelayCommand(AddNumber);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _screenVal;
        public string ScreenVal
        {
            get { return _screenVal; }
            set
            {
                _screenVal = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddNumberCommand { get; set; }
        private void AddNumber(object obj)
        {
            MessageBox.Show("Add Number clicked");
        }
    }
}
