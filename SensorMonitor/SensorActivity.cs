using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Window;
using AndroidX.Activity;
using AndroidX.AppCompat.App;
using AndroidX.Core.View;
using SensorMonitor.App;
using SensorMonitor.Fragments;
using SensorMonitor.Model;
using SensorMonitor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Java.Util.Jar.Attributes;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace SensorMonitor
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    internal class SensorActivity : AppCompatActivity
    {
        MySensor mySensor;
        Sensor sensor;
        SensorData sensorData = new SensorData();
        LocalData localData = new LocalData();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string name = Intent.GetStringExtra("sensor");

            if (name != null)
            {
                mySensor = LocalData.mySensorList.Find(x => x.name == name);
                sensor = sensorData.GetSensor(mySensor.getType());
            }

            SetContentView(Resource.Layout.activity_sensor);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "My Title";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            TextView type = FindViewById<TextView>(Resource.Id.typeSensor);
            type.Text = "Sensor Type: " + mySensor.getType().ToString();
        }

        protected override void OnPause()
        {
            base.OnPause();
            localData.saveData();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.toolbar_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case 16908332:  //---Resource.Id.home не подходит, у кнопки "назад" нет имени---//
                    //Toast.MakeText(this, "Back", ToastLength.Short).Show();
                    Intent intent = new Intent(this, typeof(MainActivity));
                    intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
                    StartActivity(intent);
                    return true;
                case Resource.Id.menu_favorite:
                    //Toast.MakeText(this, "Add to Favorite", ToastLength.Short).Show();                    
                    if (mySensor.isFavorite()) LocalData.mySensorList.Find(x => x.name == mySensor.getName()).removeFavorite();
                    else LocalData.mySensorList.Find(x => x.name == mySensor.getName()).addFavorite();
                    return true;
            }
         
            return base.OnOptionsItemSelected(item);
        }

        

        
    }
}