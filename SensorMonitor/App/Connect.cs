using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SensorMonitor.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SensorMonitor.App
{
    internal class Connect : Application
    {
        private EditText editIp, editPort;
        private Button btnConnect;
        private TcpClient client;

        public Connect(View view)
        {
            client = new TcpClient();
            editIp = view.FindViewById<EditText>(Resource.Id.editIp);
            editPort = view.FindViewById<EditText>(Resource.Id.editPort);
            btnConnect = view.FindViewById<Button>(Resource.Id.btnConnect);
            if(Connection.isConnected) btnConnect.Text = "Disconnect";
            btnConnect.Click += async delegate
            {
                if (!Connection.isConnected)
                {
                    try
                    {
                        await client.ConnectAsync(editIp.Text, Convert.ToInt32(editPort.Text));
                        if (client.Connected)
                        {
                            Connection.Instance.client = client;
                            Toast.MakeText(Context, "Client connected to server!", ToastLength.Short).Show();
                            btnConnect.Text = "Disconnect";
                        }
                        else
                        {
                            Toast.MakeText(Context, "Connection feild!", ToastLength.Short).Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        Toast.MakeText(Context, "Connection feild!", ToastLength.Short).Show();
                        Toast.MakeText(Context, "" + ex, ToastLength.Short).Show();
                    }
                }
                else
                {
                    Connection.Instance.client.Close();
                    Toast.MakeText(Context, "Disconnect!", ToastLength.Short).Show();
                    btnConnect.Text = "Connect";
                }
            };
        }

        public static void Transmit(byte[] buffer)
        {
            if (Connection.isConnected)
            {
                int bufferLength = buffer.Length;
                byte[] lengthBytes = BitConverter.GetBytes(bufferLength);
                NetworkStream stream;
                var client = Connection.Instance.client;
                stream = client.GetStream();

                stream.Write(lengthBytes, 0, lengthBytes.Length);
                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}