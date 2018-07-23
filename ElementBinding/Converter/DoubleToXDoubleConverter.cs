using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ElementBinding.Converter
{
    public class DoubleToXDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //Multithreading-Sicherung
            lock (this)
            {
                if (double.TryParse(value.ToString(), out double result))
                {
                    if (double.TryParse(value.ToString(), out double param))
                    {
                        return result * param;
                    }
                    return result;
                }
                throw new FormatException("Ungültige Zahl");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            lock (this)
            {
                if (double.TryParse(value.ToString(), out double result))
                {
                    if (double.TryParse(value.ToString(), out double param))
                    {
                        return result / param;
                    }
                    return result;
                }
                throw new FormatException("Ungültige Zahl");
            }           
        }
    }
}
