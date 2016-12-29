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
    public class LinkLabel : Control
    {
        public Color ActiveLinkColor { get; internal set; }
        public Color LinkColor { get; internal set; }
        public string Text { get; internal set; }
        public ContentAlignment TextAlign { get; internal set; }
        public Color VisitedLinkColor { get; internal set; }

        public event LinkLabelLinkClickedEventHandler LinkClicked;

        public class Link
        {

        }
    } 
}