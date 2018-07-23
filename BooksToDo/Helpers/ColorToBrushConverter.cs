using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace BooksToDo.Helpers
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is System.Drawing.Color dcolor)
            {
                Color color = Color.FromArgb(dcolor.A, dcolor.R, dcolor.G, dcolor.B);

                if (targetType == typeof(Brush))
                {
                    return new SolidColorBrush(color);
                }
                else if (targetType == typeof(Color))
                {
                    return color;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is SolidColorBrush brush)
            {
                Color c = brush.Color;
                return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);
            }
            else if (value is Color color)
            {
                return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            }
            return null;
        }
    }
}
