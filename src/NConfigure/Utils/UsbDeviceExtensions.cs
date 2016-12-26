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

namespace Android.Hardware.Usb
{
    public static class UsbDeviceExtensions
    {
        public static string GetDeviceInfoText(this UsbDevice device)
        {
            var sb = new StringBuilder();
            sb.Append($"DeviceId : {device.DeviceId} \n");
            sb.Append($"ProductId : {device.ProductId} \n");
            sb.Append($"ProductName : {device.ProductName} \n");
            sb.Append($"ManufacturerName : {device.ManufacturerName} \n");
            sb.Append($"SerialNumber : {device.SerialNumber} \n");
            sb.Append($"VendorId : {device.VendorId} \n");
            sb.Append($"Interface count : {device.InterfaceCount} \n");
            sb.Append($"Class : {device.Class } \n");
            sb.Append($"SubClass : {device.DeviceSubclass } \n");
            sb.Append("\r\n");

            for (var i = 0; i < device.InterfaceCount; i++)
            {
                var usbInterface = device.GetInterface(i);
                sb.Append("\n  Interface " + i);
                sb.Append("\n\tInterface ID: " + usbInterface.Id);
                sb.Append("\n\tClass: " + usbInterface.InterfaceClass);
                sb.Append("\n\tProtocol: " + usbInterface.InterfaceProtocol);
                sb.Append("\n\tSubclass: " + usbInterface.InterfaceSubclass);
                sb.Append("\n\tEndpoint count: " + usbInterface.EndpointCount);

                for (int j = 0; j < usbInterface.EndpointCount; j++)
                {
                    sb.Append("\n\t  Endpoint " + j);
                    sb.Append("\n\t\tAddress: " + usbInterface.GetEndpoint(j).Address);
                    sb.Append("\n\t\tAttributes: " + usbInterface.GetEndpoint(j).Attributes);
                    sb.Append("\n\t\tDirection: " + usbInterface.GetEndpoint(j).Direction);
                    sb.Append("\n\t\tNumber: " + usbInterface.GetEndpoint(j).EndpointNumber);
                    sb.Append("\n\t\tInterval: " + usbInterface.GetEndpoint(j).Interval);
                    sb.Append("\n\t\tType: " + usbInterface.GetEndpoint(j).Type);
                    sb.Append("\n\t\tMax packet size: " + usbInterface.GetEndpoint(j).MaxPacketSize);
                }
            }

            return sb.ToString();
        }
    }
}