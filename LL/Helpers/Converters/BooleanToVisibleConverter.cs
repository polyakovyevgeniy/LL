using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace LL.Helpers.Converters
{
    /// <summary>
    /// Конвертирует из типа булеан в тип Visibility
    /// </summary>
    [ValueConversion(typeof(Boolean), typeof(Visibility))]
    class BooleanToVisibleConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((Boolean)value == true)
                return Visibility.Hidden;
            else return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (((Visibility)value) == Visibility.Visible)
                return false;
            else return true;
        }
    }
}
