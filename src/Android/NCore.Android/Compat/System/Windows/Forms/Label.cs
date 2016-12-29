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
using Android.Util;
using System.Drawing;

namespace System.Windows.Forms
{
    public class Label : Control
    {
        public object BorderStyle { get; set; }

        public ContentAlignment ImageAlign { get; set; }

        public String Text { get; set; }
     
        public ContentAlignment TextAlign { get; set; }
    }
}