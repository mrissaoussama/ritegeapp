using System;
using System.Globalization;
using Xamarin.Forms;

namespace ritegeapp.Converters
{
    public class BoolToColorOnlineStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int?)value is not null)
                return Color.Green;
            return Color.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
