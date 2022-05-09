    using RitegeDomain.DTO;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ritegeapp.Converters
{
    public class TypeTicketToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType()==typeof(TypeTicket))
            { TypeTicket typeticket = (TypeTicket)value;
                if (TypeTicket.TicketStationnement == typeticket)
                {
                    return "Stationnement";
                }
             
            }
            return "Ticket";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

