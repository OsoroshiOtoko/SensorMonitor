using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Window;
using AndroidX.Activity;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.View.Menu;
using AndroidX.Core.View;
using SensorMonitor.App;
using SensorMonitor.Fragments;
using SensorMonitor.Model;
using SensorMonitor.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using static Java.Util.Jar.Attributes;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace SensorMonitor
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    internal class SensorActivity : AppCompatActivity, ISensorEventListener
    {
        private string nameInit;
        //private float[] values;
        private int[] id;

        private TextView type, version, value;
        private Drawable imgFavorite;
        private MySensor mySensor;
        private SensorManager sensorManager;
        private Sensor sensor;
        private SensorData sensorData = new SensorData();
        private LocalData localData = new LocalData();
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            sensorManager = GetSystemService(SensorService) as SensorManager;

            nameInit = Intent.GetStringExtra("sensor");

            if (nameInit != null)
            {
                mySensor = LocalData.mySensorList.Find(x => x.name == nameInit);
                sensor = sensorManager.GetDefaultSensor(mySensor.getType());
            }

            if (sensor != null) sensorManager.RegisterListener(this, sensor, SensorDelay.Normal);

           

            SetContentView(Resource.Layout.activity_sensor);

            InitToolbar();
            if (sensor != null) InitView();
        }

        protected override void OnResume()
        {
            base.OnResume();
         
            
        }

        protected override void OnPause()
        {
            base.OnPause();
            //localData.saveData();
            sensorManager.UnregisterListener(this);
        }

        public void InitToolbar()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = nameInit;
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public void InitView()
        {

            type = FindViewById<TextView>(Resource.Id.typeSensor);
            type.Text = "Sensor Type: " + sensor.StringType;

            version = FindViewById<TextView>(Resource.Id.versionSensor);
            version.Text = "Sensor Version: " + sensor.Version.ToString();            
        }
        
        public void InitValueView(int count)
        {
            id = new int[count];
            int i = 0;
            LinearLayout valuesLayout = FindViewById<LinearLayout>(Resource.Id.valuesContainer);
            foreach (var val in id)
            {
                TextView textView = new TextView(this);
                textView.Id = View.GenerateViewId();
                id[i] = textView.Id;
                textView.SetTextSize(Android.Util.ComplexUnitType.Sp, 24);
                textView.LayoutParameters = new TableLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent, 1f);
                textView.Gravity = GravityFlags.Center;
                textView.Text = "0.0";
                textView.SetTextColor(Color.Black);
                valuesLayout.AddView(textView);
                i++;
            }
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            MenuItemImpl fav = menu.FindItem(Resource.Id.menu_favorite) as MenuItemImpl;
            imgFavorite = fav.Icon;
            imgFavorite.Mutate();
            if(mySensor.isFavorite()) imgFavorite.SetColorFilter(Color.Yellow, PorterDuff.Mode.SrcAtop);
            else imgFavorite.SetColorFilter(Color.White, PorterDuff.Mode.SrcAtop);
            return base.OnPrepareOptionsMenu(menu);
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
                    if (mySensor.isFavorite())
                    {
                        LocalData.mySensorList.Find(x => x.name == mySensor.getName()).removeFavorite();
                        imgFavorite.SetColorFilter(Color.White, PorterDuff.Mode.SrcAtop);
                    }
                    else
                    {
                        LocalData.mySensorList.Find(x => x.name == mySensor.getName()).addFavorite();
                        imgFavorite.SetColorFilter(Color.Yellow, PorterDuff.Mode.SrcAtop);
                    }
                    return true;
            }
         
            return base.OnOptionsItemSelected(item);
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            
        }

        public void OnSensorChanged(SensorEvent ev)
        {
            TextView textView;
            if (id == null) InitValueView(ev.Values.Count);

            int i = 0;
            
            foreach(var val in ev.Values)
            {
                textView = FindViewById<TextView>(id[i]);
                textView.Text = val.ToString();
                i++;
            }

        }
    }
}