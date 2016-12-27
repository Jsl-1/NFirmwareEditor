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
using NToolbox.Models;
using System.Windows.Forms;

namespace NToolbox.Windows
{
    class PowerCurveProfileWindow : IDisposable
    {
        PowerCurveProfileWindow(ArcticFoxConfiguration.PowerCurve curve)
        {

        }

        public void Dispose()
        {
           
        }

        public DialogResult ShowDialog()
        {
            return DialogResult.Cancel;
        }
    }
}