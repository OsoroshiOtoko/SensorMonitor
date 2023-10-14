using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace SensorMonitor.Model
{
    public class MySensor
    {
        public string sensorName { get; set; }
        public SensorType sensorType { get; set; }

        [JsonIgnore]
        public bool favorite { get; set; }

        public MySensor()
        {
            sensorName = "name";
            sensorType = default;
            favorite = false;
        }

        public MySensor(string name, SensorType type, bool fav)
        {

            sensorName = name;
            sensorType = type;
            favorite = fav;
        }

        public MySensor(string name, SensorType type)
        {

            sensorName = name;
            sensorType = type;
            favorite = false;
        }

        public MySensor(string name)
        {

            sensorName = name;
            sensorType = default;
            favorite = false;
        }

        public string getName()
        {
            return sensorName;
        }

        public void setName(string name)
        {
            sensorName = name;
        }

        public SensorType getType()
        {
            return sensorType;
        }

        public void setType(SensorType type)
        {
            sensorType = type;
        }

        public bool isFavorite()
        {
            return favorite;
        }

        public void addFavorite()
        {
            favorite = true;
        }

        public void removeFavorite()
        {
            favorite = false;
        }
    }
}