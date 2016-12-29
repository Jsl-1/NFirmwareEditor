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

namespace NCore
{
    public static class InfoBox
    {
        public static void Show(string text)
        {
            Toast.MakeText(Application.Context, text, ToastLength.Long);           
        }

        public static DialogResult Show(string text, MessageBoxButtons buttons)
        {
            throw new NotImplementedException("Dialog not implemented");
            //return MessageBox.Show(text, @"Information", buttons, MessageBoxIcon.Information);
        }

        public static void Show(string format, params object[] args)
        {
            Show(string.Format(format, args));
        }
    }
}