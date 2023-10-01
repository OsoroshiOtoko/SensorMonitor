using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using Google.Android.Material.BottomNavigation;
using Java.Lang;
using SensorMonitor.Adapters;
using SensorMonitor.Fragments;
using SensorMonitor.Model;
using SensorMonitor.Services;
using System;
using System.Collections.Generic;
using static SensorMonitor.Services.SensorDataService;

namespace SensorMonitor
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnItemSelectedListener
    {
        SensorsFragment fragment;
        List<Sensor> sensorList;
        SensorDataService sensorService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            sensorService = new SensorDataService(this);
           
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            


            //fragment = new SensorsFragment();
            //SupportFragmentManager.BeginTransaction().Replace(Resource.Id.main_fragment, fragment).Commit();


            //RecyclerView recyclerView = FindViewById<RecyclerView>(Resource.Id.list);
            //SensorListAdapter sensorAdapter = new SensorListAdapter(sensors);
            //recyclerView.SetAdapter(sensorAdapter);




            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnItemSelectedListener(this);
        }

        
        protected override void OnResume()
        {
            base.OnResume();

            fragment = new SensorsFragment(sensorService.GetSensors());
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.main_fragment, fragment).Commit();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

   

    public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_sensors:
                    //sensorAdapter.setList(sensors);
                    return true;
                case Resource.Id.navigation_favorites:
                    //sensorAdapter.setList(fav);
                    return true;
                case Resource.Id.navigation_settings:
                    return true;
            }
            return false;
        }


    }
}

