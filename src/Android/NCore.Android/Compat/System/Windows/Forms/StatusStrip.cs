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
    public class StatusStrip : Control
    {
        public Padding GripMargin { get; internal set; }
        public List<ToolStripItem> Items { get; } = new List<ToolStripItem>();
        public bool SizingGrip { get; internal set; }
        public string Text { get; internal set; }
    }

    public class ToolStripStatusLabel : ToolStripItem
    {
        public bool Spring { get; internal set; }
        public string Text { get; internal set; }
        public ContentAlignment TextAlign { get; internal set; }
    }

    public class ToolStripItem : Control
    {

    }
}