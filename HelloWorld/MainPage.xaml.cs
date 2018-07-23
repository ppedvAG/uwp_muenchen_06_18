using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using helper = HelperLibrary;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HelloWorld
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            
            this.InitializeComponent();
            this.Background = new SolidColorBrush(Color.FromArgb(250, 250, 0, 0));
            this.Background = new SolidColorBrush(Colors.Blue);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            helper.Helper.ShowMessage("Hello World");
            Button btn1 = new Button
            {
                Content = "Button 1",
                FontSize = 40,
                Margin = new Thickness(20, 20, 0, 0)
            };
            Button btn2 = new Button
            {
                Content = "Button2",
                FontSize = 40,
                HorizontalAlignment = HorizontalAlignment.Right
            };

            btn1.Click += Button1_Click;

            Grid grid = new Grid();
            grid.Children.Add(btn1);
            grid.Children.Add(btn2);
            this.Content = grid;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Exit();
        }
    }
}
