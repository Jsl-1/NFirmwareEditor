using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Hardware.Usb;

namespace NConfigure
{
    [Activity(Label = "NConfigure", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const string ACTION_USB_PERMISSION = "com.android.example.USB_PERMISSION";
        private UsbManager m_UsbManager;
        private Button m_ButtonTest;
        private TextView m_UsbTextView;
        private UsbReceiver m_UsbReceiver;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            m_ButtonTest = FindViewById<Button>(Resource.Id.ButtonTest);
            m_ButtonTest.Click += ButtonTest_Click;

            m_UsbTextView = FindViewById<TextView>(Resource.Id.UsbTextView);

            m_UsbManager = (UsbManager)Application.Context.GetSystemService(Context.UsbService);
            m_UsbReceiver = new UsbReceiver();

            var intent = PendingIntent.GetBroadcast(this, 0, new Intent(ACTION_USB_PERMISSION), 0);
            IntentFilter filter = new IntentFilter(ACTION_USB_PERMISSION);
            filter.AddAction(UsbManager.ActionUsbDeviceAttached);
            filter.AddAction(UsbManager.ActionUsbDeviceDetached);
            RegisterReceiver(m_UsbReceiver, filter);
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
                m_UsbTextView.Append("\r\n");
            }
            //UsbAccessory[] usbTempList = usbManager.GetAccessoryList();
            //m_UsbTextView.Text = string.Empty;
            //foreach (var accessory in usbTempList)
            //{
            //    m_UsbTextView.Append($"{accessory.Manufacturer} - {accessory.Model} - {accessory.Version}");
            //}

        }
    }

    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { Android.Hardware.Usb.UsbManager.ActionUsbAccessoryAttached })]
    public class UsbReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {

        }
    }
}

