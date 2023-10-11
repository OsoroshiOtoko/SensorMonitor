using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SensorMonitor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorMonitor.App
{
    internal class LocalData : Application
    {
        internal static List<MySensor> mySensorList = new List<MySensor>();
        private MySensorJSON JSON = new MySensorJSON();

        
        public void loadData()
        {
            mySensorList.Clear();
            var read = JSON.readJSON();

            if (read == null)
            {
                SensorManager sensorManager = Context.GetSystemService(SensorService) as SensorManager;
                List<Sensor> sensors = new List<Sensor>(sensorManager.GetSensorList(SensorType.All));
                foreach (var sensor in sensors)
                {
                    mySensorList.Add(new MySensor(sensor.Name, sensor.Type));
                }
            }
            else mySensorList = read;
        }

        public void saveData() => JSON.writeJSONAsync(mySensorList).GetAwaiter();
    }
}