using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace ritegeapp.Converters
{
    public class IntToBoolStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine(parameter);
            if (value is null)
                return false;
            if ((string)parameter == "NoConnection")

                return !((int)value > -1);
            return ((int)value > -1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
