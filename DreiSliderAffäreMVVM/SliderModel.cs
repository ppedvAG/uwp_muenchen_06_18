using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DreiSliderAffäreMVVM
{
    public class SliderModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<string> ShowAlert = null;

        private int _slider1 = 10;

        public int Slider1
        {
            get { return _slider1; }
            set
            {
                _slider1 = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Slider1)));
            }
        }

        private int _slider2 = 50;

        public int Slider2
        {
            get { return _slider2; }
            set
            {
                _slider2 = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Slider2)));
            }
        }

        private int _slider3 = 30;


        public int Slider3
        {
            get { return _slider3; }
            set
            {
                _slider3 = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Slider3)));
            }
        }

        public ICommand BerechneSummeCommand { get; set; }

        public SliderModel()
        {
            BerechneSummeCommand = new OneParameterCommand(p =>
            {
                int summe = Slider1 + Slider2 + Slider3;
                ShowAlert?.Invoke(this, summe.ToString());
            });
        }
    }

    public class OneParameterCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> _executeMethod; 
        private Func<object, bool> _canExecuteMethod; 

        public OneParameterCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod = null)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            if(_canExecuteMethod != null)
            {
                return _canExecuteMethod(parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            _executeMethod?.Invoke(parameter);
        }

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
