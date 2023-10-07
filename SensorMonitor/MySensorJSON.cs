using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using SensorMonitor.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SensorMonitor
{
    internal class MySensorJSON : Application
    {
        private string path = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "MySensors.json");

        //internal delegate void MakeText(string text);
        //private static MakeText? toast;
     
        
        /*
        public void RegisterToast(MakeText del)
        {
            toast = del;
        }
        */

        public async Task writeJSONAsync(List<MySensor> mySensors) 
        {
            string _json = JsonSerializer.Serialize(mySensors);            

            try
            {
                using (var writer = File.CreateText(path))
                {
                    await writer.WriteLineAsync(_json);
                }

                //toast?.Invoke("json write");
                Toast.MakeText(Context, "json write", ToastLength.Short).Show();
            } 
            catch (IOException ex)
            {
                //toast?.Invoke(ex.Message);
                Toast.MakeText(Context, ex.Message, ToastLength.Short).Show();
            }           
        }

        public List<MySensor> readJSON() 
        {
            try
            {
                using (var reader = new StreamReader(path, true))
                {
                    string data;
                    while ((data = reader.ReadLine()) != null)
                    {
                        //json = data;
                        List<MySensor> mySensors = JsonSerializer.Deserialize<List<MySensor>>(data);
                        //toast?.Invoke("json read");
                        Toast.MakeText(Context, "json read", ToastLength.Short).Show();

                        return mySensors;
                    }
                }
            }
            catch (IOException ex)
            {
                //toast?.Invoke(ex.Message);
                Toast.MakeText(Context, ex.Message, ToastLength.Short).Show();
            }         

            return null;
        }

        
    }
}