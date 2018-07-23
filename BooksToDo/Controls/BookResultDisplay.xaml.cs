using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TodoServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace BooksToDo.Controls
{
    public sealed partial class BookResultDisplay : UserControl
    {

        private bool _changed = false;

        public List<Book> BookItemsSource
        {
            get { return (List<Book>)GetValue(BookItemsSourceProperty); }
            set { SetValue(BookItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BookItemsSourceProperty =          
            DependencyProperty.Register(nameof(BookItemsSource), typeof(List<Book>), typeof(BookResultDisplay), new PropertyMetadata(null, BookItemsSourceChange));

        private static void BookItemsSourceChange(DependencyObject obj, DependencyPropertyChangedEventArgs args )
        {
            //Auf Änderung reagieren
            if(obj is BookResultDisplay display)
            {
                display._changed = true;
            }
        }

        public BookResultDisplay()
        {
            this.InitializeComponent();
        }

        //Anstelle der Visual State Triggers kann das Adaptive Layout auch über EventHandler im Code-Behind realisiert werden
        private void ListBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is ListBox list)
            {
                if(e.NewSize.Width < 500)
                {
                    list.ItemTemplate = this.Resources["shortBookTemplate"] as DataTemplate;
                }
                else
                {
                    list.ItemTemplate = this.Resources["broadBookTemplate"] as DataTemplate;
                }
            }
        }
    }
}
