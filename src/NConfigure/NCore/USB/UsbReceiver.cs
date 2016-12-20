using Android.App;
using Android.Content;
using Android.Hardware.Usb;
using System;
using System.Collections.Generic;

namespace NConfigure
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { Android.Hardware.Usb.UsbManager.ActionUsbAccessoryAttached })]
    public class HidUsbReceiver : BroadcastReceiver
    {
        private const int VendorId = 0x0416;
        //private const int ProductId = 

        private  NCore.USB.HidConnector m_Connector;

        private UsbManager m_UsbManager;
        private UsbDevice m_Device;
        private UsbDeviceConnection mConnection;
        private UsbEndpoint mEndpointRead;
        private UsbEndpoint mEndpointWrite;

        public event Action<bool> DeviceConnected;

        public HidUsbReceiver()
        {
            m_UsbManager = (UsbManager)Application.Context.GetSystemService(Context.UsbService);
        }

        public HidUsbReceiver(UsbManager usbManager)
        {
            m_UsbManager = usbManager;
            m_Connector = new NCore.USB.HidConnector();
        }
        
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action == UsbManager.ActionUsbDeviceAttached)
            {
                foreach (var device in m_UsbManager.DeviceList)
                {
                    if (device.Value.VendorId == VendorId)
                    {
                        m_Device = (UsbDevice)intent.GetParcelableExtra(UsbManager.ExtraDevice);
                        m_UsbManager.RequestPermission(m_Device, intent.)
                        if (intent.GetBooleanExtra(UsbManager.ExtraPermissionGranted, false))
                        {
                            var connected = _SetHIDDevice(device);

                            if (DeviceConnected != null)
                                DeviceConnected(connected);

                            return;

                        }

                    }
                    
                }
            }

            m_Device = null;
            if (intent.Action == UsbManager.ActionUsbDeviceDetached)
            {
                DeviceConnected(false);
            }
           
        }

        private bool _SetHIDDevice(KeyValuePair<string, UsbDevice> device)
        {


           m_Device = device.Value;

            if (device.Value.InterfaceCount != 1)
                return false;

           var usbInterface = m_Device.GetInterface(0);

           if (usbInterface.EndpointCount != 2)
                return false;


            mEndpointRead = usbInterface.GetEndpoint(0);
            mEndpointWrite = usbInterface.GetEndpoint(1);

            //check that we should be able to read and write
            

             UsbDeviceConnection connection = m_UsbManager.OpenDevice(m_Device);
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

