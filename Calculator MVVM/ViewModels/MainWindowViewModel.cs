using Calculator_MVVM.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            AddOperationCommand = new RelayCommand(AddOperation, CanAddOperation);
            ClearScreenCommand = new RelayCommand(ClearScreen);
            GetResultCommand = new RelayCommand(GetResult, CanGetResult);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<string> _availableOperations = new List<string> { "+", "-", "/", "*" };
        private DataTable _dataTable = new DataTable();
        private bool isLastSignAnOperation;
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
        public ICommand AddOperationCommand { get; set; }
        public ICommand ClearScreenCommand { get; set; }
        public ICommand GetResultCommand { get; set; }
        private void AddNumber(object obj)
        {
            var number = obj as string;

            if (ScreenVal == "0" && number != ",")
                ScreenVal = string.Empty;
            else if (number == "," && _availableOperations.Contains(ScreenVal.Substring(ScreenVal.Length - 1)))
                number = "0,";

            if (number == "," && ScreenVal.Substring(ScreenVal.Length - 1) == ",") { }
              else  ScreenVal += number;

            isLastSignAnOperation = false;
        }

        private void AddOperation(object obj)
        {
            var operation = obj as string;
            ScreenVal += operation;

            isLastSignAnOperation = true;
        }

        private void ClearScreen(object obj)
        {
            ScreenVal = "0";
            isLastSignAnOperation = false;
        }
        private void GetResult(object obj)
        {
            var result = Math.Round(Convert.ToDouble(_dataTable.Compute(ScreenVal.Replace("," , "."), "")),2);
            ScreenVal = result.ToString();
        }

        private bool CanGetResult(object obj)
        {
            return !isLastSignAnOperation;
        }
        private bool CanAddOperation(object obj)
        {
            return !isLastSignAnOperation;
        }
    }
}
