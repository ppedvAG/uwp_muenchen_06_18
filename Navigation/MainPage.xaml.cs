using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Navigation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static List<string> PersonenListe = new List<string>()
        {
            "Alf",
            "Uschi",
            "Uwe"
        };

        public MainPage()
        {
            this.InitializeComponent();
            personenListe.ItemsSource = PersonenListe;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (personenListe.SelectedItem != null)
            {
                if(personenListe.SelectedItem is ListBoxItem item)
                {
                    string person = item.Content.ToString();
                    this.Frame.Navigate(typeof(DetailPage), person);
                }
                this.Frame.Navigate(typeof(DetailPage), new NameChange(personenListe.SelectedIndex, personenListe.SelectedItem.ToString()));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is NameChange c)
            {
                PersonenListe[c.Index] = c.Name;
            }
            base.OnNavigatedTo(e);
        }

        private void personenListe_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Button_Click(this, null);
        }
    }

    public class NameChange
    {
       
        public int Index { get; set; }
        public string Name { get; set; }

        public NameChange(int index, string name)
        {
            Index = index;
            Name = name;
        }
    }
}
