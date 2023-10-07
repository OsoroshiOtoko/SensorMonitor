using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SensorMonitor.Model;
using SensorMonitor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorMonitor.App
{
    internal class LocalData
    {
        internal static List<MySensor> mySensorList = new List<MySensor>();
        MySensorJSON JSON = new MySensorJSON();
        SensorData sensorData = new SensorData();
        

        public void loadData()
        {
            mySensorList.Clear();
            var read = JSON.readJSON();

            if (read == null)
            {
                foreach (var _sensor in sensorData.GetSensors())
                {
                    mySensorList.Add(new MySensor(_sensor.Name, _sensor.Type));
                }
            }
            else mySensorList = read;
        }

        public void saveData() => JSON.writeJSONAsync(mySensorList).GetAwaiter();
    }
}