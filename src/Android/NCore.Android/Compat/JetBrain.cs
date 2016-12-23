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

namespace JetBrains.Annotations
{
    public class CanBeNullAttribute : Attribute
    {
    }

    public class NotNullAttribute : Attribute
    {
    }
}