using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using AndroidX.RecyclerView.Widget;
using SensorMonitor.Fragments;
using SensorMonitor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorMonitor.Adapters
{
    internal class SensorListAdapter : RecyclerView.Adapter
    {
        private List<MySensor> mySensors = new List<MySensor>();
        //private List<Sensor> sensors;  

        public SensorListAdapter(List<Sensor> _sensors)
        {
            foreach (var sensor in _sensors)
            {
                mySensors.Add(new MySensor(sensor.Name, sensor.Type));
            }
        }

        public override int ItemCount
        {
            get { return mySensors.Count; }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_sensor, parent, false);
            return new SensorViewHolder(itemView);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            SensorViewHolder viewHolder = holder as SensorViewHolder;
            viewHolder.Name.Text = mySensors[position].getName();
        }

        public class SensorViewHolder : RecyclerView.ViewHolder
        {
            public TextView Name { get; set; }

            public SensorViewHolder(View itemView) : base(itemView)
            {
                Name = itemView.FindViewById<TextView>(Resource.Id.name);
            }
        }


        public void AddItem(MySensor _mySensor)
        {
            mySensors.Add(_mySensor);
            NotifyDataSetChanged();
        }

        public void AddItem(Sensor _sensor)
        {
            mySensors.Add(new MySensor(_sensor.Name, _sensor.Type));
            NotifyDataSetChanged();
        }

        public void RemoveItem(int index)
        {
            mySensors.RemoveAt(index);
            NotifyDataSetChanged();
        }


        public void SetList(List<MySensor> _mySensors)
        {
            mySensors = _mySensors;
            NotifyDataSetChanged();
        }

        public void SetList(List<Sensor> _sensors)
        {
            foreach (var sensor in _sensors)
            {
                mySensors.Add(new MySensor(sensor.Name, sensor.Type));
            }
            NotifyDataSetChanged();
        }

        public void ClearList()
        {
            mySensors.Clear();
            NotifyDataSetChanged();
        }
    }
}