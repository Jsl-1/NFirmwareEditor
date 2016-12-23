using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;
using Android.Hardware.Usb;

namespace HidSharp
{
    public sealed class HidStream : IDisposable
    {
        private readonly object locker = new object();
        private UsbDevice m_Device;
        private UsbInterface m_UsbInterface;
        private UsbEndpoint m_EndPointRead;
        private UsbEndpoint m_EndPointWrite;
        private UsbDeviceConnection m_Connection;
        private Handler m_UiHandler = new Handler();


        private System.Collections.Concurrent.ConcurrentStack<byte> m_ReceivedBytes = new System.Collections.Concurrent.ConcurrentStack<byte>();

        public HidStream(UsbDevice device)
        {
            m_Device = device;

            var usbManager = (UsbManager)Application.Context.GetSystemService(Context.UsbService);
            m_Connection = usbManager.OpenDevice(m_Device);
            m_UsbInterface = m_Device.GetInterface(0);
            if (m_Connection == null)
            {

                //Unable to establish connection
            }

            for (var i = 0; i < m_UsbInterface.EndpointCount; i++)
            {
                var endpoint = m_UsbInterface.GetEndpoint(i);
                if (endpoint.Direction == UsbAddressing.Out)
                {
                    m_EndPointWrite = endpoint;
                    MaxOutputReportLength = endpoint.MaxPacketSize + 1;
                }
                if (endpoint.Direction == UsbAddressing.In)
                {
                    m_EndPointRead = endpoint;
                    MaxInputReportLength = endpoint.MaxPacketSize + 1;
                }
            }
            if (m_EndPointRead == null)
                System.Diagnostics.Debug.WriteLine("Unable to get endpoint for reading");
            if (m_EndPointWrite == null)
                System.Diagnostics.Debug.WriteLine("Unable to get endpoint for writing");
        }

        public int MaxOutputReportLength { get; internal set; }

        public int MaxInputReportLength { get; internal set; }


        public HidStream Open()
        {
            if(!m_Connection.ClaimInterface(m_UsbInterface, true))
            {
                throw new Exception("Can't lock interface");
            }
            return this;
        }
        public void Write(byte[] buffer)
        {
            lock (locker)
            {

                var bytes = new byte[buffer.Length - 1];
                Buffer.BlockCopy(buffer, 1, bytes, 0, bytes.Length);
                //int transfer = m_Connection.ControlTransfer((UsbAddressing)0x21, 0x09, 0x00, 0x01, buffer, buffer.Length, 0);
                //transfer = m_Connection.ControlTransfer((UsbAddressing)0xA1, 0x01, 0x00, 0x01, buffer, buffer.Length, 0);
                int status = m_Connection.BulkTransfer(m_EndPointWrite, bytes, bytes.Length, 250);
            }
        }
        public void Read(byte[] value)
        {
            var data = new byte[value.Length - 1];

            m_Connection.BulkTransfer(m_EndPointRead, data, data.Length, 1000);

            Buffer.BlockCopy(data, 0, value, 1, data.Length);
        }

        public void Dispose()
        {
           // m_ShouldStop = true;
           // m_ReceiverThread.Join();
            if (m_Connection != null)
            {
                m_Connection.ReleaseInterface(m_UsbInterface);
                m_Connection.Close();
                m_Connection.Dispose();
                m_Connection = null;
            }
           
        }

     

        //public void onUSBDataReceive(byte[] buffer)
        //{
        //    foreach(var b in buffer)
        //        m_ReceivedBytes.Append(b);
        //}

        //private void RunDataReceive()
        //{
        //    try
        //    {
        //        if (m_Connection != null && m_EndPointRead != null)
        //        {
        //            while (!m_ShouldStop)
        //            {
        //                var buffer = new Byte[MaxInputReportLength];
        //                var status = m_Connection.BulkTransfer(m_EndPointRead, buffer, MaxInputReportLength, 100);
        //                if (status > 0)
        //                {
        //                    m_UiHandler.Post(new Java.Lang.Runnable(() => onUSBDataReceive(buffer)));
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //}

    }

  
}


//public class HidBridge
//{
//    private Context _context;
//    private int _productId;
//    private int _vendorId;

 
//    private const String ACTION_USB_PERMISSION = "com.example.company.app.testhid.USB_PERMISSION";
	
//	// Locker object that is responsible for locking read/write thread.
//	private Object _locker = new Object();
//    private Thread _readingThread = null;
//    private String _deviceName;

//    private UsbManager _usbManager;
//    private UsbDevice _usbDevice;

//    // The queue that contains the read data.
//    private Queue<byte[]> _receivedQueue;

//    /**
//	 * Creates a hid bridge to the dongle. Should be created once.
//	 * @param context is the UI context of Android.
//	 * @param productId of the device.
//	 * @param vendorId of the device.
//	 */
//    public HidBridge(Context context, int productId, int vendorId)
//    {
//        _context = context;
//        _productId = productId;
//        _vendorId = vendorId;
//        _receivedQueue = new Queue<byte[]>();
//    }

//    /**
//	 * Searches for the device and opens it if successful
//	 * @return true, if connection was successful
//	 */
//    public Boolean OpenDevice()
//    {
//        _usbManager = (UsbManager)_context.GetSystemService(Context.UsbService);

//        IDictionary<String, UsbDevice> deviceList = _usbManager.DeviceList;

//        _usbDevice = null;
//        foreach (var device in deviceList.Values)
//        {
//            if (device.ProductId == _productId && device.VendorId == _vendorId)
//            {
//                _usbDevice = device;
//                _deviceName = _usbDevice.DeviceName;
//            }
//        }

//        if (_usbDevice == null)
//        {
//            return false;
//        }

//        // Create and intent and request a permission.
//        PendingIntent mPermissionIntent = PendingIntent.GetBroadcast(_context, 0, new Intent(ACTION_USB_PERMISSION), 0);
//        IntentFilter filter = new IntentFilter(ACTION_USB_PERMISSION);
//        _context.RegisterReceiver(mUsbReceiver, filter);

//        _usbManager.requestPermission(_usbDevice, mPermissionIntent);
//        Log("Found the device");
//        return true;
//    }

//    /**
//	 * Closes the reading thread of the device.
//	 */
//    public void CloseTheDevice()
//    {
//        StopReadingThread();
//    }

//    /**
//	 * Starts the thread that continuously reads the data from the device. 
//	 * Should be called in order to be able to talk with the device.
//	 */
//    public void StartReadingThread()
//    {
//        if (_readingThread == null)
//        {
//            _readingThread = new Thread(readerReceiver);
//            _readingThread.start();
//        }
//        else
//        {
//            Log("Reading thread already started");
//        }
//    }

//    /**
//	 * Stops the thread that continuously reads the data from the device.
//	 * If it is stopped - talking to the device would be impossible.
//	 */
//    @SuppressWarnings("deprecation")

//    public void StopReadingThread()
//    {
//        if (_readingThread != null)
//        {
//            // Just kill the thread. It is better to do that fast if we need that asap.
//            _readingThread.stop();
//            _readingThread = null;
//        }
//        else
//        {
//            Log("No reading thread to stop");
//        }
//    }

//    /**
//	 * Write data to the usb hid. Data is written as-is, so calling method is responsible for adding header data.
//	 * @param bytes is the data to be written.
//	 * @return true if succeed.
//	 */
//    public boolean WriteData(byte[] bytes)
//    {
//        try
//        {
//            // Lock that is common for read/write methods.
//            synchronized(_locker) {
//                UsbInterface writeIntf = _usbDevice.getInterface(0);
//                UsbEndpoint writeEp = writeIntf.getEndpoint(1);
//                UsbDeviceConnection writeConnection = _usbManager.openDevice(_usbDevice);

//                // Lock the usb interface.
//                writeConnection.claimInterface(writeIntf, true);

//                // Write the data as a bulk transfer with defined data length.
//                int r = writeConnection.bulkTransfer(writeEp, bytes, bytes.length, 0);
//                if (r != -1)
//                {
//                    Log(String.format("Written %s bytes to the dongle. Data written: %s", r, composeString(bytes)));
//                }
//                else
//                {
//                    Log("Error happened while writing data. No ACK");
//                }

//                // Release the usb interface.
//                writeConnection.releaseInterface(writeIntf);
//                writeConnection.close();
//            }

//        }
//        catch (NullPointerException e)
//        {
//            Log("Error happend while writing. Could not connect to the device or interface is busy?");
//            Log.e("HidBridge", Log.getStackTraceString(e));
//            return false;
//        }
//        return true;
//    }

//    /**
//	 * @return true if there are any data in the queue to be read.
//	 */
//    public boolean IsThereAnyReceivedData()
//    {
//        synchronized(_locker) {
//            return !_receivedQueue.isEmpty();
//        }
//    }

//    /**
//	 * Queue the data from the read queue.
//	 * @return queued data.
//	 */
//    public byte[] GetReceivedDataFromQueue()
//    {
//        synchronized(_locker) {
//            return _receivedQueue.poll();
//        }
//    }

//    // The thread that continuously receives data from the dongle and put it to the queue.
//    private Runnable readerReceiver = new Runnable()
//    {

//        public void run()
//    {
//        if (_usbDevice == null)
//        {
//            Log("No device to read from");
//            return;
//        }

//        UsbEndpoint readEp;
//        UsbDeviceConnection readConnection = null;
//        UsbInterface readIntf = null;
//        boolean readerStartedMsgWasShown = false;

//        // We will continuously ask for the data from the device and store it in the queue.
//        while (true)
//        {
//            // Lock that is common for read/write methods.
//            synchronized(_locker) {
//                try
//                {
//                    if (_usbDevice == null)
//                    {
//                        OpenDevice();
//                        Log("No device. Recheking in 10 sec...");

//                        Sleep(10000);
//                        continue;
//                    }

//                    readIntf = _usbDevice.getInterface(0);
//                    readEp = readIntf.getEndpoint(0);
//                    if (!_usbManager.getDeviceList().containsKey(_deviceName))
//                    {
//                        Log("Failed to connect to the device. Retrying to acquire it.");
//                        OpenDevice();
//                        if (!_usbManager.getDeviceList().containsKey(_deviceName))
//                        {
//                            Log("No device. Recheking in 10 sec...");

//                            Sleep(10000);
//                            continue;
//                        }
//                    }

//                    try
//                    {

//                        readConnection = _usbManager.openDevice(_usbDevice);

//                        if (readConnection == null)
//                        {
//                            Log("Cannot start reader because the user didn't gave me permissions or the device is not present. Retrying in 2 sec...");
//                            Sleep(2000);
//                            continue;
//                        }

//                        // Claim and lock the interface in the android system.
//                        readConnection.claimInterface(readIntf, true);
//                    }
//                    catch (SecurityException e)
//                    {
//                        Log("Cannot start reader because the user didn't gave me permissions. Retrying in 2 sec...");

//                        Sleep(2000);
//                        continue;
//                    }

//                    // Show the reader started message once.
//                    if (!readerStartedMsgWasShown)
//                    {
//                        Log("!!! Reader was started !!!");
//                        readerStartedMsgWasShown = true;
//                    }

//                    // Read the data as a bulk transfer with the size = MaxPacketSize
//                    int packetSize = readEp.getMaxPacketSize();
//                    byte[] bytes = new byte[packetSize];
//                    int r = readConnection.bulkTransfer(readEp, bytes, packetSize, 50);
//                    if (r >= 0)
//                    {
//                        byte[] trancatedBytes = new byte[r]; // Truncate bytes in the honor of r

//                        int i = 0;
//                        for (byte b : bytes)
//                        {
//                            trancatedBytes[i] = b;
//                            i++;
//                        }

//                        _receivedQueue.add(trancatedBytes); // Store received data
//                        Log(String.format("Message received of lengths %s and content: %s", r, composeString(bytes)));
//                    }

//                    // Release the interface lock.
//                    readConnection.releaseInterface(readIntf);
//                    readConnection.close();
//                }

//                catch (NullPointerException e)
//                {
//                    Log("Error happened while reading. No device or the connection is busy");
//                    Log.e("HidBridge", Log.getStackTraceString(e));
//                }
//                catch (ThreadDeath e)
//                {
//                    if (readConnection != null)
//                    {
//                        readConnection.releaseInterface(readIntf);
//                        readConnection.close();
//                    }

//                    throw e;
//                }
//            }

//            // Sleep for 10 ms to pause, so other thread can write data or anything. 
//            // As both read and write data methods lock each other - they cannot be run in parallel.
//            // Looks like Android is not so smart in planning the threads, so we need to give it a small time
//            // to switch the thread context.
//            Sleep(10);
//        }
//    }
//};

//private void Sleep(int milliseconds)
//{
//    try
//    {
//        Thread.sleep(milliseconds);
//    }
//    catch (InterruptedException e)
//    {
//        e.printStackTrace();
//    }
//}

//private final BroadcastReceiver mUsbReceiver = new BroadcastReceiver()
//{


//        public void onReceive(Context context, Intent intent)
//{
//    String action = intent.getAction();
//    if (ACTION_USB_PERMISSION.equals(action))
//    {
//        synchronized(this) {
//            UsbDevice device = (UsbDevice)intent.getParcelableExtra(UsbManager.EXTRA_DEVICE);

//            if (intent.getBooleanExtra(UsbManager.EXTRA_PERMISSION_GRANTED, false))
//            {
//                if (device != null)
//                {
//                    //call method to set up device communication
//                }
//            }
//            else
//            {
//                Log.d("TAG", "permission denied for the device " + device);
//            }
//        }
//    }
//}
//	};
	
//	/**
//	 * Logs the message from HidBridge.
//	 * @param message to log.
//	 */
//	private void Log(String message)
//{
//    LogHandler logHandler = LogHandler.getInstance();
//    logHandler.WriteMessage("HidBridge: " + message, LogHandler.GetNormalColor());
//}

///**
// * Composes a string from byte array.
// */
//private String composeString(byte[] bytes)
//{
//    StringBuilder builder = new StringBuilder();
//    for (byte b: bytes)
//    {
//        builder.append(b);
//        builder.append(" ");
//    }

//    return builder.toString();
//}
//}