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
    public class PictureBox : Control
    {
        public PictureBoxSizeMode SizeMode { get; set; }

        public Android.Graphics.Drawables.Drawable BackgroundImage { get; set; }

        public Android.Graphics.Drawables.Drawable Image { get; set; }

        public ImageLayout BackgroundImageLayout { get; internal set; }
    }
}