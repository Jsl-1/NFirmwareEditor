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

        private  NCore.USB.HidConnector m_Connector;

        private UsbManager m_UsbManager;
        private UsbDevice m_Device;
        private UsbDeviceConnection mConnectionRead;
        private UsbDeviceConnection mConnectionWrite;
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
                    if( device.Value.VendorId == VendorId)
                        m_Device = device.Value;
                    var connected = _SetHIDDevice(device);
                    if (DeviceConnected != null)
                        DeviceConnected(connected);
                    return;
                }
            }
            if (intent.Action == UsbManager.ActionUsbDeviceDetached)
            {
                m_Device = null;
            }
            DeviceConnected(false);
        }

        private bool _SetHIDDevice(KeyValuePair<string, UsbDevice> device)
        {
            //UsbInterface usbInterfaceRead = null;
            //UsbInterface usbInterfaceWrite = null;
            //UsbEndpoint ep1 = null;
            //UsbEndpoint ep2 = null;
            //Boolean UsingSingleInterface = true;

            //m_Device = device.Value;

           
            //usbInterfaceRead = m_Device.GetInterface(0x00);
            //usbInterfaceWrite = m_Device.GetInterface(0x01);
            //if ((usbInterfaceRead.EndpointCount == 1) && (usbInterfaceWrite.EndpointCount == 1))
            //{
            //    ep1 = usbInterfaceRead.GetEndpoint(0);
            //    ep2 = usbInterfaceWrite.GetEndpoint(0);
            //}            


            ////because ep1 = ep2 this will now not cause a return unless no ep is found at all
            //if ((ep1 == null) || (ep2 == null))
            //{
            //    return false;
            //}

            //// Determine which endpoint is the read, and which is the write
            //if (ep1.Type == UsbConstants.USB_ENDPOINT_XFER_INT)//I am getting a return of 3, which is an interrupt transfer
            //{
            //    if (ep1.Direction ==  UsbAddressing.In )//I am getting a return of 128, which is a device-to-host endpoint
            //    {
            //        mEndpointRead = ep1;                 
            //    }
            //    if (ep1.UsbAddressing.In)//nope
            //    {
            //        mEndpointWrite = ep1;
            //        if (DEBUG == 6)
            //        {
            //            Toast.makeText(UsbHidDeviceTesterActivity.this, "EP1 is a write", Toast.LENGTH_LONG).show();
            //        }
            //    }
            //}

            ////if (ep2.getType() == UsbConstants.USB_ENDPOINT_XFER_INT)
            ////{
            ////    if (ep2.getDirection() == UsbConstants.USB_DIR_IN)
            ////    {
            ////        //Try treating it as a write anyway             
            ////        //mEndpointRead = ep2;
            ////        mEndpointWrite = ep2;
            ////    }
            ////    else if (ep2.getDirection() == UsbConstants.USB_DIR_OUT)
            ////    {
            ////        //usbEndpointWrite = ep2;
            ////        mEndpointWrite = ep2;
            ////    }
            ////}

            //////check that we should be able to read and write
            ////if ((mEndpointRead == null) || (mEndpointWrite == null))
            ////{
            ////    return false;
            ////}
            ////if (device != null)
            ////{
            ////    UsbDeviceConnection connection = m_UsbManager.openDevice(device);
            ////    if (connection != null && connection.claimInterface(usbInterfaceRead, true))
            ////    {
            ////        Log.d(TAG, "open SUCCESS");
            ////        mConnectionRead = connection;
            ////        // Start the read thread
            ////        //Comment out while desperately attempting to write on this connection/interface
            ////        //Thread thread = new Thread(this);
            ////        //thread.start();

            ////    }
            ////    else
            ////    {
            ////        Log.d(TAG, "open FAIL");
            ////        mConnectionRead = null;
            ////    }
            ////}
            ////if (UsingSingleInterface)
            ////{
            ////    mConnectionWrite = mConnectionRead;
            ////}
            ////else //! UsingSingleInterface
            ////{
            ////    mConnectionWrite = m_UsbManager.openDevice(device);
            ////    mConnectionWrite.claimInterface(usbInterfaceWrite, true);
            ////}
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

