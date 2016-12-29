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

namespace System.Windows.Forms
{
    class WindowsFormsUtils
    {
        public static int RotateLeft(int value, int nBits)
        {
            nBits = nBits % 32;
            return value << (nBits & 31) | value >> (32 - nBits & 31);
        }
    }
}