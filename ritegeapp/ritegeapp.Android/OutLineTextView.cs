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
[assembly: ExportRenderer(typeof(OutLineLabel), typeof(OutLineTextView))]

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
                if(!string.IsNullOrEmpty(e.NewElement.Text))
                strokeTextView.Text = e.NewElement.Text;

                else
                                    if (!string.IsNullOrEmpty(e.NewElement.FormattedText.ToString()))
                    strokeTextView.Text = e.NewElement.FormattedText.ToString();

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
                if (!string.IsNullOrEmpty(label.Text))
                    strokeTextView.Text = label.Text;
                else
                                    if (!string.IsNullOrEmpty(label.FormattedText.ToString()))

                    strokeTextView.Text = label.FormattedText.ToString();
                SetNativeControl(strokeTextView);
            }
        }
    }
}