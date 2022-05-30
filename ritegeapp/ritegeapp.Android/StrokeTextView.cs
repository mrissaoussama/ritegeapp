using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using ritegeapp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Graphics.Paint;

namespace ritegeapp.Droid
{
    class StrokeTextView : TextView
    {
        public TextView borderText = null;

        public StrokeTextView(Context context, Utils.OutLineLabel label) : base(context)
        {
            borderText = new TextView(context);

            init(label);
        }

     
        public StrokeTextView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            borderText = new TextView(context, attrs);
            init();
        }
        public StrokeTextView(Context context, IAttributeSet attrs, int defStyle) : base(context,
        attrs, defStyle)
        {
            borderText = new TextView(context, attrs, defStyle);
            init();
        }


        private void init(OutLineLabel label)
        {
            TextPaint tp1 = borderText.Paint;
            tp1.StrokeWidth = 5;         // sets the stroke width                        
            tp1.SetStyle(Style.Stroke);
            SetTextColor(Color.ParseColor(label.TextColor.ToHex()));
            borderText.SetTextColor(Color.ParseColor(label.StrokeColor));  // set the stroke color
            borderText.Gravity = Gravity;

        }

        public void init()
        {

            TextPaint tp1 = borderText.Paint;
            tp1.StrokeWidth = 5;         // sets the stroke width                        
            tp1.SetStyle(Style.Stroke);
            borderText.SetTextColor(Color.White);  // set the stroke color
            borderText.Gravity = Gravity;

        }


        public override ViewGroup.LayoutParams LayoutParameters
        {
            get => base.LayoutParameters;
            set => base.LayoutParameters = value;
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            string tt = borderText.Text;


            if (tt == null || !tt.Equals(this.Text))
            {
                borderText.Text = Text;
                this.PostInvalidate();
            }

            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            borderText.Measure(widthMeasureSpec, heightMeasureSpec);
        }

        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            base.OnLayout(changed, left, top, right, bottom);
            borderText.Layout(left, top, right, bottom);
        }

        protected override void OnDraw(Canvas canvas)
        {
            borderText.Draw(canvas);
            base.OnDraw(canvas);
        }
    }
}