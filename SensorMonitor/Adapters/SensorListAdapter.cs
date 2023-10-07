using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using AndroidX.RecyclerView.Widget;
using SensorMonitor.App;
using SensorMonitor.Fragments;
using SensorMonitor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Views.View;
using static AndroidX.RecyclerView.Widget.RecyclerView;

namespace SensorMonitor.Adapters
{
    internal class SensorListAdapter : RecyclerView.Adapter
    {
        private List<MySensor> mySensors = new List<MySensor>();

        public event EventHandler<int> ItemClick;

        public SensorListAdapter(List<MySensor> _mySensors)
        {
            mySensors = _mySensors; 
            
        }


        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_sensor, parent, false);
            return new SensorViewHolder(itemView, OnClick);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MySensor mySensor = mySensors[position];
            SensorViewHolder viewHolder = holder as SensorViewHolder;
            viewHolder.nameView.Text = mySensor.getName();

            
        }

        public override int ItemCount
        {
            get { return mySensors.Count; }
        }

        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }


        public class SensorViewHolder : RecyclerView.ViewHolder
        {
            public TextView nameView { get; set; }

            public SensorViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
                nameView = itemView.FindViewById<TextView>(Resource.Id.name);

                itemView.Click += (sender, e) => listener(LayoutPosition);
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

        public List<MySensor> GetList() 
        {
            return mySensors;
        }

        public void ClearList()
        {
            mySensors.Clear();
            NotifyDataSetChanged();
        }

        
    }
}