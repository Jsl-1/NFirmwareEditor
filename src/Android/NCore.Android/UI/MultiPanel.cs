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
using System.Windows.Forms;

namespace NCore.UI
{
    public class MultiPanel : Control
    {
        public MultiPanelPage SelectedPage { get; set; }
    }

    public class MultiPanelPage : Control
    {
        public object Description { get; internal set; }
        public string Text { get; internal set; }
    }
}