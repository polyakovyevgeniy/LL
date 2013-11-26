using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LL.Helpers.Converters
{
    /// <summary>
    /// Конвертирует True В False и обратно
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    class TrueToFalseConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value == true)
                return false;
            else return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value == true)
                return true;
            else return false;
        }
    }
}
