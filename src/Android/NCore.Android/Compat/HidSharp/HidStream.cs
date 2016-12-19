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

namespace HidSharp
{
    public sealed class HidStream : IDisposable
    {
        public int MaxOutputReportLength { get; internal set; }

        public int MaxInputReportLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public HidStream Open()
        {
            throw new NotImplementedException();
        }

        public void Read(byte[] value)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Write(byte[] buffer)
        {
            throw new NotImplementedException();
        }
    }
}