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
    public class DataPoint
    {
        private double factor;
        private double temperature;

        public DataPoint(double temperature, double factor)
        {
            this.temperature = temperature;
            this.factor = factor;
        }

        public string Label { get; internal set; }
        public int MarkerSize { get; internal set; }
        public MarkerStyle MarkerStyle { get; internal set; }
        public string Tag { get; internal set; }
        public double XValue { get; internal set; }
        public double[] YValues { get; internal set; }
    }

    public class ChartArea
    {
        public Axis AxisY { get; set; }

        public Axis AxisX { get; set; }
    }

    public class Chart : Control
    {
        public List<ChartArea> ChartAreas { get; } = new List<ChartArea>();
        public ChartColorPalette Palette { get; internal set; }
        public SeriesCollection Series { get; } = new SeriesCollection();

        public HitTestResult[] HitTest(int x, int y, bool v, ChartElementType dataPoint)
        {
            throw new NotImplementedException();
        }
    }

    public class Grid
    {
        public bool Enabled { get; internal set; }
        public Color LineColor { get; internal set; }
    }
}