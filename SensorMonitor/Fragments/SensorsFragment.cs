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
    public class SensorsFragment : Fragment
    {
        private RecyclerView recyclerView;
        private SensorDataService sensorDataService;
        private List<Sensor> sensors;

        //private SensorAdapter adapter;

        public SensorsFragment() { }

        public SensorsFragment(List<Sensor> _sensors) 
        {
            sensors = _sensors;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.fragment_sensors, container, false);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.list);
            SensorListAdapter adapter = new SensorListAdapter(sensors);
            recyclerView.SetAdapter(adapter);

            return view;
        }
        



        
    }
}