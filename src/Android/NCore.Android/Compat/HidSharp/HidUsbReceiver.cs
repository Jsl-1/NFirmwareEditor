using Android.App;
using Android.Content;
using Android.Hardware.Usb;
using System;
using System.Collections.Generic;

namespace NCore.USB
{

    [BroadcastReceiver(Enabled = false)]
    //[IntentFilter(new[] {
    //    Android.Hardware.Usb.UsbManager.ActionUsbDeviceAttached,
    //    Android.Hardware.Usb.UsbManager.ActionUsbDeviceDetached })]
    //[MetaData(Android.Hardware.Usb.UsbManager.ActionUsbDeviceAttached, Resource = "@xml/usb_device_filter")]
    //[MetaData(Android.Hardware.Usb.UsbManager.ActionUsbDeviceDetached, Resource = "@xml/usb_device_filter")]
    public class HidUsbReceiver : BroadcastReceiver
    {
        public const string ACTION_USB_PERMISSION = "com.android.example.USB_PERMISSION";

        private UsbDeviceConnection mConnection;
        private UsbEndpoint mEndpointRead;
        private UsbEndpoint mEndpointWrite;

        public event Action<UsbDevice, bool> DeviceConnected;

        public HidUsbReceiver()
        {

        }

        public override void OnReceive(Context context, Intent intent)
        {
            var usbManager = (UsbManager)context.GetSystemService(Context.UsbService);
            if (intent.Action == ACTION_USB_PERMISSION)
            {
                lock (this)
                {
                    UsbDevice device = (UsbDevice)intent.GetParcelableExtra(UsbManager.ExtraDevice);
                    if (device != null)
                    {
                       
                        bool hasPermision = usbManager.HasPermission(device);
                        if (!hasPermision)
                        {
                            hasPermision = intent.GetBooleanExtra(UsbManager.ExtraPermissionGranted, false);
                        }
                        if (hasPermision)
                        {
                            HidConnector.Instance.RefreshState();
                            return;
                        } 
                    }
                }
            }
            if (intent.Action == UsbManager.ActionUsbDeviceAttached)
            {
                HidConnector.Instance.RefreshState();
            }
            if (intent.Action == UsbManager.ActionUsbDeviceDetached)
            {
                HidConnector.Instance.RefreshState();
            }
          
        }

        private void _RaiseDeviceConnected(UsbDevice device, bool connected)
        {
            if (DeviceConnected != null)
                DeviceConnected(device, connected);
        }

       
        private bool _SetHIDDevice(UsbDevice device, UsbManager manager)
        {
            if (device.InterfaceCount != 1)
                return false;

           var usbInterface = device.GetInterface(0);

           if (usbInterface.EndpointCount != 2)
                return false;


            mEndpointRead = usbInterface.GetEndpoint(0);
            mEndpointWrite = usbInterface.GetEndpoint(1);

            //check that we should be able to read and write
             UsbDeviceConnection connection = manager.OpenDevice(device);
             if (connection != null && connection.ClaimInterface(usbInterface, true))
             {
                 mConnection = connection;
                return true;
             }

            mConnection = null;
            return false;
        }

        //// searches for an interface on the given USB device
        //private UsbInterface findInterface(UsbDevice device)
        //{
           
          
        //    for (int i = 0; i < device.InterfaceCount; i++)
        //    {
        //        UsbInterface intf = device.GetInterface(i);
        //        String InterfaceInfo = intf.ToString();
                
        //        //Class below is 3 for USB_HID
        //        if (intf.InterfaceClass == UsbClass.Hid)
        //        {
        //            return intf;
        //        }
        //    }

        //    return null;
        //}

        //private boolean sendControlTransfer(byte[] dataToSend)
        //{
        //    synchronized(this)
        //    {
        //        if (mConnectionRead != null)
        //        {
        //            //byte[] message = new byte[13];  // or 14?
        //            byte[] message = dataToSend;
        //            if (DEBUG == 9)
        //            {
        //                Toast.makeText(UsbHidDeviceTesterActivity.this, "Sending Control Transfer", Toast.LENGTH_LONG).show();
        //            }

        //            //first field ox21 is bin 00100001 which splits into 0 01 00001 for direction(1bit)/type(2b)/recipient(5b)
        //            //To set direction as 'host to Device' we need 0, To set type to HID we need 11 (3), and for recipient we want 00001
        //            //second field 0x09 is class specific request code, 0x09 is listed as 'reserved for future use'
        //            //third field 0x200 is value
        //            //int transfer = mConnectionRead.controlTransfer(0x21, 0x9, 0x200, 0, message, message.length, 0);
        //            //try with type set to HID
        //            int transfer = mConnectionRead.controlTransfer(0xC1, 0x9, 0x200, 0, message, message.length, 0);
        //            if (DEBUG == 10)
        //            {
        //                Toast.makeText(UsbHidDeviceTesterActivity.this, "Transfer returned " + transfer, Toast.LENGTH_LONG).show();
        //            }
        //        }
        //    }
        //    return true;
        //}


        //private boolean sendInterruptTransfer(byte[] dataToSend)
        //{
        //    int bufferDataLength = mEndpointWrite.getMaxPacketSize();//The write endpoint is null unless we just copy the read endpoint
        //    if (DEBUG == 12)
        //    {
        //        Toast.makeText(UsbHidDeviceTesterActivity.this, "Max Packet Size: " + bufferDataLength, Toast.LENGTH_LONG).show();
        //    }

        //    ByteBuffer buffer = ByteBuffer.allocate(bufferDataLength + 1);
        //    UsbRequest request = new UsbRequest();
        //    buffer.put(dataToSend);

        //    request.initialize(mConnectionWrite, mEndpointWrite);
        //    request.queue(buffer, bufferDataLength);

        //    try
        //    {
        //        /* only use requestwait on a read
        //        if (request.equals(mConnectionWrite.requestWait()))
        //        {
        //            return true;
        //        }
        //        */
        //    }
        //    catch (Exception ex)
        //    {
        //        // An exception has occurred
        //        if (DEBUG == 13)
        //        {
        //            Toast.makeText(UsbHidDeviceTesterActivity.this, "Caught Write Exception", Toast.LENGTH_LONG).show();
        //        }
        //    }

        //    return true;
        //}
    }
}

