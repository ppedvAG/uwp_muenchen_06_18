using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ResourcenUndStyles
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //TODO: Style
            btn1.RegisterPropertyChangedCallback(Button.StyleProperty , delegate
            {
                int z = 5;
            });

            //btn1.FontSize = 5;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["pageBackground"] = new SolidColorBrush(Colors.Lavender);
            //this.Background = (SolidColorBrush)Application.Current.Resources["pageBackground"];
            this.Frame.Navigate(typeof(MainPage));
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            if(sender is MenuFlyoutItem item)
            {
                string fileName = item.Tag.ToString();
                //Pack Uris: https://docs.microsoft.com/de-de/windows/uwp/app-resources/uri-schemes
                Application.Current.Resources.MergedDictionaries[0] = new ResourceDictionary() { Source = new Uri("ms-appx:///" + fileName) };
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            //Flyout code-seitig anzeigen
            if(sender is Button btn)
            {
                btn.Flyout.ShowAt(btn);
            }
        }

        private void MenuFlyoutItem_Click_Style(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem item)
            {
                string fileName = item.Tag.ToString();
                //Pack Uris: https://docs.microsoft.com/de-de/windows/uwp/app-resources/uri-schemes

                

                //Das entsprechende Item in den MergedDictionaries muss mit einer neuzen Instanz von ResourceDictionary überschrieben
                //werden damit sofort ein Effekt sichtbar wird!
                Application.Current.Resources.MergedDictionaries[1] = new ResourceDictionary() { Source = new Uri("ms-appx:///" + fileName) };
            }
        }

        private void Theme_Change_Click_Light(object sender, RoutedEventArgs e)
        {

            this.RequestedTheme = ElementTheme.Light;
        }

        private void Theme_Change_Click_Dark(object sender, RoutedEventArgs e)
        {
            this.RequestedTheme = ElementTheme.Dark;

        }
    }
}
