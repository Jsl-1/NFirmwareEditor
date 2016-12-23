//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Android.Hardware.Usb;

//namespace NConfigure.Compat.HidSharp
//{
//    public class HidHelper
//    {
//        private static HidUsbReceiver s_HidUsbReceiver;

//        static HidHelper()
//        {
//            var applicationContext = Application.Context;
//            IntentFilter filter = new IntentFilter(HidUsbReceiver.ACTION_USB_PERMISSION);
//            filter.AddAction(UsbManager.ActionUsbDeviceAttached);
//            filter.AddAction(UsbManager.ActionUsbDeviceDetached);
//            filter.AddAction(UsbManager.ActionUsbDeviceDetached);

//            s_HidUsbReceiver = new HidUsbReceiver();
//            s_HidUsbReceiver.DeviceConnected += UsbReceiver_DeviceConnected;

//            RegisterReceiver(s_HidUsbReceiver, filter);
//        }



//    }



   
//}