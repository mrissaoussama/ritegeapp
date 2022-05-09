using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ritegeapp.Converters
{
    public class IntToProgressConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double current; 
            double max; 
           

            current = System.Convert.ToDouble(values[0]);
            max = System.Convert.ToDouble(values[1]);
            if (max == 0 || current > max||current==-1||max==-1)
                return 0.0d;
            return (current/max);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
