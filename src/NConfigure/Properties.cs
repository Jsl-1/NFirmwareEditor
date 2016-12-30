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
using System.IO;
using Android.Media;
using Android.Graphics.Drawables;
using System.Resources;

namespace NToolbox.Properties
{
    public static class Resources
    {
        public static byte[] new_configuration
        {
            get
            {
                using (var stream = typeof(Resources).Assembly.GetManifestResourceStream("NToolbox.Resources.raw.new_configuration.afc"))
                using (var ms = new MemoryStream())
                {

                    stream.CopyTo(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    return ms.ToArray();
                }
            }
        }

        public static Drawable arctic_fox_logo
        {
            get
            {
                return Application.Context.Resources.GetDrawable(Resource.Drawable.arctic_fox_logo, Application.Context.Theme);
            }
        }

    }
}