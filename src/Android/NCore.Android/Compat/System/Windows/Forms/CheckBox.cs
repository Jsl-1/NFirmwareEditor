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
    public class CheckBox : Control
    {
        public Boolean Checked { get; set; }
      
        public string Text { get; set; }

        public bool UseVisualStyleBackColor { get; set; }
    }
}