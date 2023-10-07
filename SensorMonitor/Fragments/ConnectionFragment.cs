using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using AndroidX.RecyclerView.Widget;
using SensorMonitor.Adapters;
using SensorMonitor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorMonitor;
using SensorMonitor.Services;
using Android.Hardware;


namespace SensorMonitor.Fragments
{
    public class ConnectionFragment : Fragment
    {          


        public ConnectionFragment() 
        {
            
        }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.fragment_connection, container, false);            

            return view;
        }
        



        
    }
}