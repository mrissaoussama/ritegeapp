using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using ritegeapp.Droid;
using ritegeapp.Extentions;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessDatePicker), typeof(BorderlessDatePickerRenderer))]
namespace ritegeapp.Droid
{
    public class BorderlessDatePickerRenderer : DatePickerRenderer
    {
        public static void Init() { }
        public BorderlessDatePickerRenderer(Context context) : base(Android.App.Application.Context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control == null || e.NewElement == null|| Element==null||e.OldElement!=null) return;
            //for example ,change the line to red:
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Red);
            else
                Control.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var entry = (BorderlessDatePicker)sender;
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(entry.UnderlineColor.ToAndroid());
            else
                Control.Background.SetColorFilter(entry.UnderlineColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
        }
    }
}