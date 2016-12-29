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

namespace System.Windows.Forms.DataVisualization.Charting
{
    public class Series
    {
        public int BorderWidth { get; set; }
        public SeriesChartType ChartType { get; set; }
        public Color Color { get; set; }
        public ChartValueType YValueType { get; set; }
        public SmartLabelStyle SmartLabelStyle { get;  set; }
        public bool Enabled { get; internal set; }
        public LabelOutsidePlotAreaStyle AllowOutsidePlotArea { get; internal set; }

        public List<DataPoint> Points { get; } = new List<DataPoint>();
        public string Name { get; internal set; }
    }
}