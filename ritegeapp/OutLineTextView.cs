using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ritegeapp.Droid;
using ritegeapp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace ritegeapp.Droid
{
    public class OutLineTextView : LabelRenderer
    {
        Context context;
        public OutLineTextView(Context context) : base(context)
        {
            this.context = context;
        }




        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var label = ((OutLineLabel)e.NewElement);

                StrokeTextView strokeTextView = new StrokeTextView(context,label);
                strokeTextView.Text = e.NewElement.Text;
                SetNativeControl(strokeTextView);
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control != null)
            {
                var label = ((OutLineLabel)sender);

                StrokeTextView strokeTextView = new StrokeTextView(context,label);
                strokeTextView.TextSize = (float)label.FontSize;
                strokeTextView.borderText.TextSize = (float)label.FontSize;
                strokeTextView.Text = label.Text;
                SetNativeControl(strokeTextView);
            }
        }
    }
}