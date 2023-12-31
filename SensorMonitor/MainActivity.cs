﻿using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;
using SensorMonitor.Fragments;
using SensorMonitor.Model;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Widget;
using SensorMonitor.App;
using SensorMonitor.Adapters;
using Android.Hardware;
using System;
using System.Net.Sockets;

namespace SensorMonitor
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnItemSelectedListener
    {
        Connect connect;
        EditText editIp, editPort;
        Button btnConnect;
        TcpClient client;
        MySensorJSON JSON = new MySensorJSON();
        LocalData localData = new LocalData();
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //JSON.RegisterToast(ActivityToast);

            localData.loadData();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.main_fragment, new SensorsFragment(LocalData.mySensorList, OnItemClick)).Commit();


            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnItemSelectedListener(this);
        }

        
        protected override void OnResume()
        {
            base.OnResume();

            //localData.loadData();
        }

        protected override void OnPause()
        {
            base.OnPause();

            localData.saveData();
        }

        

        protected override void OnDestroy()
        {
            base.OnDestroy();

            localData.saveData();
        }
 
        

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        void OnItemClick(object sender, int position)
        {
            SensorListAdapter adapter = sender as SensorListAdapter;
            MySensor mySensor = adapter.GetItem(position);
            //Toast.MakeText(Application.Context, "ID = " + position + "  Name = " + mySensor.getName(), ToastLength.Short).Show();

            Intent intent = new Intent(this, typeof(SensorActivity));
            intent.PutExtra("sensor", mySensor.getName());
            StartActivity(intent);
    }


        public bool OnNavigationItemSelected(IMenuItem item)
        {
            List<MySensor> _mySensors = new List<MySensor>(LocalData.mySensorList);

            switch (item.ItemId)
            {
                case Resource.Id.navigation_sensors:
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.main_fragment, new SensorsFragment(LocalData.mySensorList, OnItemClick)).Commit();
                    return true;
                case Resource.Id.navigation_favorites:
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.main_fragment, new SensorsFragment(LocalData.mySensorList.FindAll(x => x.favorite == true), OnItemClick)).Commit();
                    return true;
                case Resource.Id.navigation_connection:
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.main_fragment, new ConnectionFragment()).Commit();
                    return true;
            }

            return false;
        }

        //public void ActivityToast(string text) => Toast.MakeText(this, text, ToastLength.Short).Show();
    }
}

