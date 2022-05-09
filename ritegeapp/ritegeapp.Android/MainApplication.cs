using Android.App;
using Android.OS;
using Android.Runtime;
using System;
using System.Collections.Generic;

namespace ritegeapp.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
        
        }

    }
}
