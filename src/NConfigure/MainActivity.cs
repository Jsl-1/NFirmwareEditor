using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Hardware.Usb;
using System.Collections.Generic;

namespace NConfigure
{
    [Activity(Label = "NConfigure", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const string ACTION_USB_PERMISSION = "com.android.example.USB_PERMISSION";
        private UsbManager m_UsbManager;
        private Button m_ButtonTest;
        private TextView m_UsbTextView;
        private HidUsbReceiver m_HidUsbReceiver;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            m_ButtonTest = FindViewById<Button>(Resource.Id.ButtonTest);
            m_ButtonTest.Click += ButtonTest_Click;

            m_UsbTextView = FindViewById<TextView>(Resource.Id.UsbTextView);

            m_UsbManager = (UsbManager)Application.Context.GetSystemService(Context.UsbService);
            m_HidUsbReceiver = new HidUsbReceiver(m_UsbManager);
            m_HidUsbReceiver.DeviceConnected += UsbReceiver_DeviceConnected;

            var intent = PendingIntent.GetBroadcast(this, 0, new Intent(ACTION_USB_PERMISSION), 0);
            IntentFilter filter = new IntentFilter(ACTION_USB_PERMISSION);
            filter.AddAction(UsbManager.ActionUsbDeviceAttached);
            filter.AddAction(UsbManager.ActionUsbDeviceDetached);
            RegisterReceiver(m_HidUsbReceiver, filter);
        }

        private void UsbReceiver_DeviceConnected(bool connected)
        {
            if (connected)
            {
                
            }
            else
            {

            }
        }

        private void ButtonTest_Click(object sender, System.EventArgs e)
        {
            m_UsbTextView.Text = string.Empty;

            var usbManager = (UsbManager)Application.Context.GetSystemService(Context.UsbService);
            foreach(var device in usbManager.DeviceList)
            {
                m_UsbTextView.Append(device.Key);
                m_UsbTextView.Append($"DeviceId : {device.Value.DeviceId} \n");
                m_UsbTextView.Append($"ProductId : {device.Value.ProductId} \n");
                m_UsbTextView.Append($"ProductName : {device.Value.ProductName} \n");
                m_UsbTextView.Append($"ManufacturerName : {device.Value.ManufacturerName} \n");
                m_UsbTextView.Append($"SerialNumber : {device.Value.SerialNumber} \n");
                m_UsbTextView.Append($"VendorId : {device.Value.VendorId} \n");
                m_UsbTextView.Append($"Version : {device.Value.Version} \n");
                m_UsbTextView.Append($"Interface count : {device.Value.InterfaceCount} \n");
                m_UsbTextView.Append($"Class : {device.Value.Class } \n");
                m_UsbTextView.Append($"SubClass : {device.Value.DeviceSubclass } \n");
                m_UsbTextView.Append("\r\n");

                for(var i = 0; i< device.Value.InterfaceCount; i++)
                {
                    var usbInterface = device.Value.GetInterface(i);
                    m_UsbTextView.Append("\n  Interface " + i);
                    m_UsbTextView.Append("\n\tInterface ID: " + usbInterface.Id);
                    m_UsbTextView.Append("\n\tClass: " + usbInterface.InterfaceClass);
                    m_UsbTextView.Append("\n\tProtocol: " + usbInterface.InterfaceProtocol);
                    m_UsbTextView.Append("\n\tSubclass: " + usbInterface.InterfaceSubclass);
                    m_UsbTextView.Append("\n\tEndpoint count: " + usbInterface.EndpointCount);

                    for (int j = 0; j < usbInterface.EndpointCount; j++)
                    {
                        m_UsbTextView.Append("\n\t  Endpoint " + j);
                        m_UsbTextView.Append("\n\t\tAddress: " + usbInterface.GetEndpoint(j).Address);
                        m_UsbTextView.Append("\n\t\tAttributes: " + usbInterface.GetEndpoint(j).Attributes);
                        m_UsbTextView.Append("\n\t\tDirection: " + usbInterface.GetEndpoint(j).Direction);
                        m_UsbTextView.Append("\n\t\tNumber: " + usbInterface.GetEndpoint(j).EndpointNumber);
                        m_UsbTextView.Append("\n\t\tInterval: " + usbInterface.GetEndpoint(j).Interval);
                        m_UsbTextView.Append("\n\t\tType: " + usbInterface.GetEndpoint(j).Type);
                        m_UsbTextView.Append("\n\t\tMax packet size: " + usbInterface.GetEndpoint(j).MaxPacketSize);
                    }
                }
                
            }

            //UsbAccessory[] usbTempList = usbManager.GetAccessoryList();
            //m_UsbTextView.Text = string.Empty;
            //foreach (var accessory in usbTempList)
            //{
            //    m_UsbTextView.Append($"{accessory.Manufacturer} - {accessory.Model} - {accessory.Version}");
            //}

        }


     
    }
}

