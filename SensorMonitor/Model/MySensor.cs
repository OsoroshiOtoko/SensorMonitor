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

namespace SensorMonitor.Model
{
    public class MySensor
    {
        public string name { get; set; }
        public SensorType type { get; set; }
        public bool favorite { get; set; }

        public MySensor()
        {
            name = "name";
            type = default;
            favorite = false;
        }

        public MySensor(string _name, SensorType _type, bool _fav)
        {

            name = _name;
            type = _type;
            favorite = _fav;
        }

        public MySensor(string _name, SensorType _type)
        {

            name = _name;
            type = _type;
            favorite = false;
        }

        public MySensor(string _name)
        {

            name = _name;
            type = default;
            favorite = false;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string _name)
        {
            name = _name;
        }

        public SensorType getType()
        {
            return type;
        }

        public void setType(SensorType _type)
        {
            type = _type;
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