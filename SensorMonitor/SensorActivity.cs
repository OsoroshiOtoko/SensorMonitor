using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorMonitor
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    internal class SensorActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            TextView textView = new TextView(this);
            textView.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);
            textView.SetPadding(16, 16, 16, 16);
            textView.SetText("SecondActivity", TextView.BufferType.Normal);
            SetContentView(textView);
        }
    }
}