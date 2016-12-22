//using Android.Content;
//using Android.Hardware.Usb;
//using System.Collections.Generic;
//using System.Threading;
//using System;
//using Android.App;
//using NConfigure;
//using Java.Lang;

/////
///// This class is used for talking to hid of the dongle, connecting, disconnencting and enumerating the devices.
///// https://gist.github.com/archeg/8333021
/////
//public class HidBridge
//{
//    private Context _context;
//    private int _productId;
//    private int _vendorId;

//    private const string ACTION_USB_PERMISSION = "com.example.company.app.testhid.USB_PERMISSION";

//    //Locker object that is responsible for locking read/write thread.
//    private System.Object _locker = new System.Object();
//    private Java.Lang.Thread _readingThread = null;
//    private string _deviceName;

//    private UsbManager _usbManager;
//    private UsbDevice _usbDevice;

//    //The queue that contains the read data.
//    private Queue<byte[]> _receivedQueue;
//    private HidUsbReceiver mUsbReceiver;
//    private Runnable readerReceiver = new Runnable();

//    /**
//	 * Creates a hid bridge to the dongle. Should be created once.
//	 * @param context is the UI context of Android.
//	 * @param productId of the device.
//	 * @param vendorId of the device.
//	 */
//    public HidBridge(Context context, int productId, int vendorId, HidUsbReceiver usbReceiver)
//    {
//        mUsbReceiver = usbReceiver;
//        _context = context;
//        _productId = productId;
//        _vendorId = vendorId;
//        _receivedQueue = new Queue<byte[]>();
//    }

//    /**
//	 * Searches for the device and opens it if successful
//	 * @return true, if connection was successful
//	 */
//    public bool OpenDevice()
//    {
//        _usbManager = (UsbManager)_context.GetSystemService(Context.UsbService);

//        IDictionary<string, UsbDevice> deviceList = _usbManager.DeviceList;
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

//       // Create and intent and request a permission.
//       PendingIntent mPermissionIntent = PendingIntent.GetBroadcast(_context, 0, new Intent(ACTION_USB_PERMISSION), 0);
//        IntentFilter filter = new IntentFilter(ACTION_USB_PERMISSION);
//        _context.RegisterReceiver(mUsbReceiver, filter);
//        _usbManager.RequestPermission(_usbDevice, mPermissionIntent);
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
//            _readingThread = new Java.Lang.Thread(readerReceiver);
//            _readingThread.Start();
//        }
//        else
//        {
//            System.Diagnostics.Debug.WriteLine("Reading thread already started");
//        }
//    }

//    ///
//	///Stops the thread that continuously reads the data from the device.
//	///If it is stopped - talking to the device would be impossible.
//	///
//    public void StopReadingThread()
//    {
//        if (_readingThread != null)
//        {
//            //Just kill the thread. It is better to do that fast if we need that asap.
//           _readingThread.Stop();
//            _readingThread = null;
//        }
//        else
//        {
//            System.Diagnostics.Debug.WriteLine("No reading thread to stop");
//        }
//    }

//    /
//	/Write data to the usb hid.Data is written as-is, so calling method is responsible for adding header data.
//	/@param bytes is the data to be written.
//	/@return true if succeed.
//	/
//    public Boolean WriteData(byte[] bytes)
//    {
//        try
//        {
//            Lock that is common for read / write methods.
//            lock (_locker)
//                {
//                    UsbInterface writeIntf = _usbDevice.GetInterface(0);
//                    UsbEndpoint writeEp = writeIntf.GetEndpoint(1);
//                    UsbDeviceConnection writeConnection = _usbManager.OpenDevice(_usbDevice);

//                    Lock the usb interface.
//                writeConnection.ClaimInterface(writeIntf, true);

//                 Write the data as a bulk transfer with defined data length.
//                int r = writeConnection.BulkTransfer(writeEp, bytes, bytes.Length, 0);
//                if (r != -1)
//                {
//                    System.Diagnostics.Debug.WriteLine(String.Format("Written %s bytes to the dongle. Data written: %s", r, bytes));
//                }
//                else
//                {
//                    System.Diagnostics.Debug.WriteLine("Error happened while writing data. No ACK");
//                }

//                 Release the usb interface.
//                writeConnection.ReleaseInterface(writeIntf);
//                writeConnection.Close();
//            }

//        }
//        catch (NullPointerException e)
//        {
//            System.Diagnostics.Debug.WriteLine("Error happend while writing. Could not connect to the device or interface is busy?");
//            return false;
//        }
//        return true;
//    }

//    /**
//	 * @return true if there are any data in the queue to be read.
//	 */
//    public bool IsThereAnyReceivedData()
//{
//    lock (_locker)
//    {
//        return !(_receivedQueue.Count == 0);
//    }
//}

///**
// * Queue the data from the read queue.
// * @return queued data.
// */
//public byte[] GetReceivedDataFromQueue()
//{
//    lock (_locker)
//    {
//        return _receivedQueue.Peek();
//    }
//}

//public void run()
//{
//    if (_usbDevice == null)
//    {
//        System.Diagnostics.Debug.WriteLine("No device to read from");
//        return;
//    }

//    UsbEndpoint readEp;
//    UsbDeviceConnection readConnection = null;
//    UsbInterface readIntf = null;
//    bool readerStartedMsgWasShown = false;

//    We will continuously ask for the data from the device and store it in the queue.
//        while (true)
//        {
//            Lock that is common for read / write methods.
//            lock (_locker)
//                {
//                    try
//                    {
//                        if (_usbDevice == null)
//                        {
//                            OpenDevice();
//                            System.Diagnostics.Debug.WriteLine("No device. Recheking in 10 sec...");

//                            Java.Lang.Thread.Sleep(10000);
//                            continue;
//                        }

//                        readIntf = _usbDevice.GetInterface(0);
//                        readEp = readIntf.GetEndpoint(0);
//                        if (!_usbManager.DeviceList.ContainsKey(_deviceName))
//                        {
//                            System.Diagnostics.Debug.WriteLine("Failed to connect to the device. Retrying to acquire it.");
//                            OpenDevice();
//                            if (!_usbManager.DeviceList.ContainsKey(_deviceName))
//                            {
//                                System.Diagnostics.Debug.WriteLine("No device. Recheking in 10 sec...");

//                                Java.Lang.Thread.Sleep(10000);
//                                continue;
//                            }
//                        }

//                        try
//                        {

//                            readConnection = _usbManager.OpenDevice(_usbDevice);

//                            if (readConnection == null)
//                            {
//                                System.Diagnostics.Debug.WriteLine("Cannot start reader because the user didn't gave me permissions or the device is not present. Retrying in 2 sec...");
//                                Java.Lang.Thread.Sleep(2000);
//                                continue;
//                            }

//                            Claim and lock the interface in the android system.
//                           readConnection.ClaimInterface(readIntf, true);
//                    }
//                    catch (SecurityException e)
//                    {
//                        System.Diagnostics.Debug.WriteLine("Cannot start reader because the user didn't gave me permissions. Retrying in 2 sec...");

//                        Java.Lang.Thread.Sleep(2000);
//                        continue;
//                    }

//                     Show the reader started message once.
//                    if (!readerStartedMsgWasShown)
//{
//    System.Diagnostics.Debug.WriteLine("!!! Reader was started !!!");
//    readerStartedMsgWasShown = true;
//}

//Read the data as a bulk transfer with the size = MaxPacketSize
//                    int packetSize = readEp.MaxPacketSize;
//byte[] bytes = new byte[packetSize];
//int r = readConnection.BulkTransfer(readEp, bytes, packetSize, 50);
//                    if (r >= 0)
//                    {
//                        byte[] trancatedBytes = new byte[r]; // Truncate bytes in the honor of r

//int i = 0;
//                        foreach (var b in bytes)
//                        {
//                            trancatedBytes[i] = b;
//                            i++;
//                        }

//                        _receivedQueue.Enqueue(trancatedBytes); // Store received data
//                        System.Diagnostics.Debug.WriteLine(string.Format("Message received of lengths %s and content: %s", r, composeString(bytes)));
//                    }

//                     Release the interface lock.
//                    readConnection.ReleaseInterface(readIntf);
//                    readConnection.Close();
//                }

//                catch (NullPointerException e)
//                {
//                    System.Diagnostics.Debug.WriteLine("Error happened while reading. No device or the connection is busy");

//                }
//                catch (ThreadDeath e)
//                {
//                    if (readConnection != null)
//                    {
//                        readConnection.ReleaseInterface(readIntf);
//                        readConnection.Close();
//                    }

//                    throw e;
//                }
//            }

//             Sleep for 10 ms to pause, so other thread can write data or anything.
//             As both read and write data methods lock each other - they cannot be run in parallel.
//             Looks like Android is not so smart in planning the threads, so we need to give it a small time
//             to switch the thread context.
//            Java.Lang.Thread.Sleep(10);
//        }
//    }
//}





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