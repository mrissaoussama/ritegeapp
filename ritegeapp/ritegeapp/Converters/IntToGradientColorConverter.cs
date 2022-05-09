using System;
using System.Globalization;
using Xamarin.Forms;

namespace ritegeapp.Converters
{
    public class IntToGradientColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var current = values[0];
            var max = values[1];
            //if (current is not null && (max is not null))
            //{
            //    Debug.WriteLine((int)current == 0);
            //    Debug.WriteLine((int)current <= (int)((int)max / 4));
            //    Debug.WriteLine((int)current <= (int)max / 2);
            //    Debug.WriteLine((int)current <= (int)max * 75 / 100);
            //    Debug.WriteLine((int)current <= (int)max * 99 / 100);
            //    Debug.WriteLine((int)current == (int)max);
            //}
            //Debug.WriteLine((current is null || max is null || (int)max == 0 || (int)current > (int)max || (int)current == -1 || (int)max == -1));
            //
            if (current is null || max is null || (int)max == 0 || (int)current > (int)max || (int)current == -1 || (int)max == -1)
                return Color.White;
            else
                switch ((int)current)
                {
                    case var _ when (int)current == 0: return Color.Red;

                    case var _ when (int)current <= (int)((int)max / 4): return Color.LightGreen;
                    case var _ when (int)current <= (int)max / 2: return Color.LightBlue;
                    case var _ when (int)current <= (int)max * 75 / 100: return Color.OrangeRed;
                    case var _ when (int)current <= (int)max * 99 / 100: return Color.Red;
                    case var _ when (int)current == (int)max: return Color.DarkRed;
                    default: return Color.White;
                }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
