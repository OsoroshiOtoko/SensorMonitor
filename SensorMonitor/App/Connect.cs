using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Nio;
using SensorMonitor.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SensorMonitor.App
{
    internal class Connect : Application
    {
        private EditText editIp, editPort;
        private Button btnConnect;
        private static TcpClient client;
        internal static bool isConnected = false;

        public Connect(View view)
        {
            editIp = view.FindViewById<EditText>(Resource.Id.editIp);
            editPort = view.FindViewById<EditText>(Resource.Id.editPort);
            btnConnect = view.FindViewById<Button>(Resource.Id.btnConnect);
            if (isConnected) btnConnect.Text = "Disconnect";
            else btnConnect.Text = "Connect";
            btnConnect.Click += async delegate
            {
                if (!isConnected)
                {
                    try
                    {
                        client = new TcpClient();
                        await client.ConnectAsync(editIp.Text, Convert.ToInt32(editPort.Text));

                        Toast.MakeText(Context, "Client connected to server!", ToastLength.Short).Show();
                        btnConnect.Text = "Disconnect";
                        isConnected = true;

                    }
                    catch (Exception ex)
                    {
                        Toast.MakeText(Context, "Connection feild!", ToastLength.Short).Show();
                        Toast.MakeText(Context, ex.Message, ToastLength.Short).Show();
                    }
                }
                else
                {
                    client.Close();
                    Toast.MakeText(Context, "Disconnect!", ToastLength.Short).Show();
                    btnConnect.Text = "Connect";
                    isConnected = false;
                }
            };
        }


        public static void Transmit(byte[] bytes, bool constLenght)
        { 
            byte[] buffer = bytes;
            if (client != null && client.Connected)
            {
                if(!constLenght) TransmitLenght(buffer);

                try
                {
                    client.GetStream().Write(buffer, 0, buffer.Length);
                }
                catch
                {
                    client.Close();
                    Toast.MakeText(Context, "Disconnect!", ToastLength.Short).Show();
                    isConnected = false;
                }
            }
            else Toast.MakeText(Context, "Client not connected!", ToastLength.Short).Show();
        }

        

        public static void TransmitLenght(byte[] bytes)
        {
            int bufferLength = bytes.Length;
            byte[] lengthBytes = BitConverter.GetBytes(bufferLength);

            try { client.GetStream().Write(lengthBytes, 0, lengthBytes.Length); }
            catch 
            {
                client.Close();
                Toast.MakeText(Context, "Disconnect!", ToastLength.Short).Show();
                isConnected = false;
            }
        }

        public static string FloatJSON(float[] array)
        {
            string[] valStr = array.Select(x => x.ToString("0.00", CultureInfo.InvariantCulture)).ToArray();
            string json = "[" + string.Join(", ", valStr) + "]";
            return json;
        }
    }
}