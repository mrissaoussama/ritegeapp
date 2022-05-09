using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ritegeapp.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string day= ((DateTime)value).ToString("dddd", new CultureInfo("fr-FR"));
            day=char.ToUpper(day[0]) + day.Substring(1);
            string month = ((DateTime)value).ToString("MMMM", new CultureInfo("fr-FR"));
            month = char.ToUpper(month[0]) + month.Substring(1);

            return (day+" " +((DateTime)value).Day+" "+month+" "+ ((DateTime)value).Year);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
