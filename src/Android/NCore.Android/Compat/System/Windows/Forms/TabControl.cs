using System;
using System.Collections.Generic;
using System.Drawing;
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
    public class TabControl : Control
    {
        public List<TabPage> TabPages { get; } = new List<TabPage>();

        public List<Control> Controls { get; } = new List<Control>();

        public Int32 TabCount { get { return TabPages.Count; } }

        public Int32 SelectedIndex { get; set; }
        public TabSizeMode SizeMode { get; internal set; }
        public Size ItemSize { get; internal set; }
    }

    public class TabPage : Control
    {
        public TabPage()
        {

        }

        public TabPage(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public List<Control> Controls { get; } = new List<Control>();
        public string Text { get; internal set; }
        public bool UseVisualStyleBackColor { get; internal set; }
    }
}