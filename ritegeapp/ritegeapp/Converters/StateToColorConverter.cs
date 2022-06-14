using RitegeDomain.DTO;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ritegeapp.Converters
{
    public class StateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dto =(Etat)value;
            {
                if (dto==Etat.Activé)
                    return Color.LightGreen;
                if (dto == Etat.Future)
                    return Color.LightBlue;
            }
            return Color.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
