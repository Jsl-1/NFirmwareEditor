using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Hardware.Usb;
using System.Collections.Generic;
using NCore.USB;
using System;

namespace NToolbox
{
    [Activity(Label = "NConfigure", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const string ACTION_USB_PERMISSION = "com.android.example.USB_PERMISSION";
        private TextView m_DebugTextView;
        private Button m_TestButton;
        private HidUsbReceiver m_HidUsbReceiver;
        private HidConnector m_HidConnector;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //DebugTextView
            m_DebugTextView = FindViewById<TextView>(Resource.Id.UsbTextView);

            //Test Button
            m_TestButton = FindViewById<Button>(Resource.Id.TestButton);
            m_TestButton.Click += M_TestButton_Click;

            m_HidConnector = new HidConnector(false);
            m_HidConnector.DeviceConnected += M_HidConnector_DeviceConnected;
            m_HidConnector.RefreshState();

            _InitUsb();
        }

        protected override void OnResume()
        {
            base.OnResume();
            m_HidConnector.RefreshState();
        }

        private void M_HidConnector_DeviceConnected(bool obj)
        {
           
        }



        private void M_TestButton_Click(object sender, System.EventArgs e)
        {
            try
            {

                if (m_HidConnector.IsDeviceConnected)
                {
                    //m_HidConnector.RestartDevice();
                    var dataflash = m_HidConnector.ReadDataflash();
                    //m_HidConnector.MakePuff(1);
                }
            }
            catch(Exception ex)
            {
                m_DebugTextView.Append("\n");
                m_DebugTextView.Text = ex.Message;
                m_DebugTextView.Append("\n");
                if (ex.InnerException != null)
                {
                    m_DebugTextView.Append(ex.InnerException.Message);
                    m_DebugTextView.Append("\n");
                }
                m_DebugTextView.Append(ex.StackTrace);

            }

        }

        private void _InitUsb()
        {
            IntentFilter filter = new IntentFilter(ACTION_USB_PERMISSION);
            filter.AddAction(UsbManager.ActionUsbDeviceAttached);
            filter.AddAction(UsbManager.ActionUsbDeviceDetached);
            filter.AddAction(UsbManager.ActionUsbAccessoryAttached);
            filter.AddAction(UsbManager.ActionUsbAccessoryDetached);
            filter.AddAction(UsbManager.ActionUsbDeviceDetached);

            m_HidUsbReceiver = new HidUsbReceiver();
            m_HidUsbReceiver.DeviceConnected += HidReceiver_DeviceConnected;

            RegisterReceiver(m_HidUsbReceiver, filter);
        }

        private void HidReceiver_DeviceConnected(UsbDevice device, bool connected)
        {
            m_DebugTextView.Text = string.Empty;
            if (device != null)
            {
                m_DebugTextView.Text = "";
                m_DebugTextView.Append("\nDevice Connected : " + connected.ToString());
                ShowDeviceInfo(device);
            }
            m_HidConnector.RefreshState();
        }

        private void ShowDeviceInfo(UsbDevice device)
        {
            m_DebugTextView.Append(device.GetDeviceInfoText());
        }
    }
}

