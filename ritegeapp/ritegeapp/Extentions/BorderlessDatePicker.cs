using Xamarin.Forms;

namespace ritegeapp.Extentions
{
    public class BorderlessDatePicker : DatePicker
    {
        public static readonly BindableProperty UnderlineColorProperty =
             BindableProperty.Create("UnderlineColor",
                                     typeof(Color),
                                     typeof(BorderlessDatePicker),
                                     Color.Black);
        public Color UnderlineColor
        {
            get { return (Color)this.GetValue(UnderlineColorProperty); }
            set { this.SetValue(UnderlineColorProperty, value); }
        }
    }
}
