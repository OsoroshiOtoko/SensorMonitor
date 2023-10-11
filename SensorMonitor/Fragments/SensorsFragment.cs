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


namespace SensorMonitor.Fragments
{
    public class SensorsFragment : Fragment
    {
        private List<MySensor> mySensors;
        private RecyclerView recyclerView;
        private SensorListAdapter adapter;
        private EventHandler<int> onItemClick;


        public SensorsFragment(List<MySensor> _mySensors, EventHandler<int> _onItemClick)
        {
            mySensors = _mySensors;
            onItemClick = _onItemClick;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.fragment_sensors, container, false);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.list);
            adapter = new SensorListAdapter(mySensors);
            adapter.ItemClick += onItemClick;
            recyclerView.SetAdapter(adapter);

            return view;
        }     
    }
}