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
using System.Drawing;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public class GroupBox : Panel
    {
        public String Text { get; set; }
    }

    [Flags]
    public enum TextFormatFlags
    {
        Left = 1,
        VerticalCenter = 4
    }
}