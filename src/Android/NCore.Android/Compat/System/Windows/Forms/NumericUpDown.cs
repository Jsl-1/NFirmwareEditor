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
using Android.Util;

namespace System.Windows.Forms
{
    public class NumericUpDown : Control
    {

        public int DecimalPlaces { get; internal set; }
        public decimal Increment { get; internal set; }
        public decimal Maximum { get; internal set; }
        public decimal Minimum { get; internal set; }
        public HorizontalAlignment TextAlign { get; internal set; }
        public Decimal Value { get; set; }

        public event EventHandler ValueChanged;

        internal void SetValue(object dimTimeout)
        {
            throw new NotImplementedException();
        }
    }

    public class TrackBar : Control
    {
        public int Maximum { get; internal set; }
        public int SmallChange { get; internal set; }
        public int TickFrequency { get; internal set; }
        public Decimal Value { get; set; }

        public event EventHandler ValueChanged;
    }
}