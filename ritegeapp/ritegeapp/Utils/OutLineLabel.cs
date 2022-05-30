using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ritegeapp.Utils
{
    public class OutLineLabel : Label
    {


        public static readonly BindableProperty StrokeColorProperty = BindableProperty.CreateAttached("StrokeColor", typeof(string), typeof(OutLineLabel), "White");
        public string StrokeColor
        {
            get { return base.GetValue(StrokeColorProperty).ToString(); }
            set { base.SetValue(StrokeColorProperty, value); }
        }

        public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.CreateAttached("StrokeThickness", typeof(int), typeof(OutLineLabel), 0);
        public int StrokeThickness
        {
            get { return (int)base.GetValue(StrokeThicknessProperty); }
            set { base.SetValue(StrokeThicknessProperty, value); }
        }
    }
}
