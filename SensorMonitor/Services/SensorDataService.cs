using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorMonitor.Model;
using Android.Hardware;
using AndroidX.AppCompat.App;
using Android.Util;
using System.Reflection.Emit;

namespace SensorMonitor.Services
{
    public class SensorDataService : Application
    {

        private List<Sensor> sensors;
        private SensorManager sensorManager;
        private Sensor sensor;
        
        public SensorDataService(Context context) 
        {
            sensorManager = context.GetSystemService(SensorService) as SensorManager;
        }


        public List<Sensor> GetSensors()
        {
            return new List<Sensor>(sensorManager.GetSensorList(SensorType.All));
        }


    }
}