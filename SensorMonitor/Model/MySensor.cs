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
    internal class MySensor
    {
        private string _name { get; set; }
        private SensorType _type { get; set; }
        private bool _isFavorite { get; set; }

        public MySensor(String name, SensorType type, bool fav)
        {

            _name = name;
            _type = type;
            _isFavorite = fav;
        }

        public MySensor(String name, SensorType type)
        {

            _name = name;
            _type = type;
            _isFavorite = false;
        }

        public MySensor(String name)
        {

            _name = name;
            _type = default;
            _isFavorite = false;
        }

        public String getName()
        {
            return _name;
        }

        public void setName(String name)
        {
            _name = name;
        }

        public SensorType getType()
        {
            return _type;
        }

        public void setType(SensorType type)
        {
            _type = type;
        }

        public bool isFavorite()
        {
            return _isFavorite;
        }

        public void addFavorite()
        {
            _isFavorite = true;
        }

        public void removeFavorite()
        {
            _isFavorite = false;
        }
    }
}