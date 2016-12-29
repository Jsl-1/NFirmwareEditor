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
    public class Button : Control
    {
        public DialogResult DialogResult { get; internal set; }
        public ContentAlignment ImageAlign { get; internal set; }
        public string Text { get; internal set; }
        public ContentAlignment TextAlign { get; internal set; }
        public bool UseVisualStyleBackColor { get; internal set; }
    }
}