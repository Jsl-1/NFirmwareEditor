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
    public class HScrollBar : Control
    {
        public int LargeChange { get; set; }
        public int Maximum { get; set; }
        public double Value { get; set; }

        public event EventHandler ValueChanged;
    }
}