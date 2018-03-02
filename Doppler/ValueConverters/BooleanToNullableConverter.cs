using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Doppler.ValueConverters
{
    public class BooleanToNullableConverter :
        IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(bool?) || !(value is bool boolValue))
                return null;

            return new bool?(boolValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(bool) || !(value is bool boolValue))
                return null;

            return boolValue;
        }
    }
}
