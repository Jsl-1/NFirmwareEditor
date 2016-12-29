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

namespace System.Windows.Forms
{
    public class TextBox : Control
    {
        public bool ReadOnly { get; set; }

        public object CharacterCasing { get; set; }

        public int MaxLength { get; set; }

        public object TextAlign { get; set; }

        public string Text { get; internal set; }
        public object SelectionStart { get; internal set; }

        public event EventHandler TextChanged;
    }
}