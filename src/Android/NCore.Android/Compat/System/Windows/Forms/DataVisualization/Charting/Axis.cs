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
    public class Axis
    {
        public bool IsMarginVisible { get; set; }
        public Color LineColor { get; set; }
        public Grid MajorGrid { get; set; }
        public double Maximum { get; set; }
        public double Minimum { get; set; }
        public double Interval { get; set; }

        public AxisScaleView ScaleView { get; } = new AxisScaleView();

        public AxisScrollBar ScrollBar { get; } = new AxisScrollBar();
        public bool IsReversed { get; internal set; }

        public double PixelPositionToValue(int x)
        {
            throw new NotImplementedException();
        }

        public double ValueToPixelPosition(double xValue)
        {
            throw new NotImplementedException();
        }
    }

    public class AxisScaleView
    {
        public bool Zoomable { get; internal set; }

        public void Zoom(object from, object to)
        {
           
        }
    }


    public class AxisScrollBar
    {
        public bool Enabled { get; internal set; }
    }
}