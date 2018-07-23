using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DreiSliderAffäreMVVM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private Slider _sliderWithFocus;

        public Slider SliderWithFocus
        {
            get { return _sliderWithFocus; }
            set
            {
                _sliderWithFocus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SliderWithFocus)));
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
            if(this.DataContext is SliderModel model)
            {
                model.ShowAlert += Model_ShowAlert;
            }
        }

        private void Model_ShowAlert(object sender, string e)
        {
            new MessageDialog(e).ShowAsync().AsTask();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Slider_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is Slider slider)
            {
                SliderWithFocus = slider;
            }
        }
    }
}