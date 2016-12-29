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

namespace System.Windows.Forms.DataVisualization.Charting
{
    public class SmartLabelStyle
    {
        public Boolean Enabled { get; set; }
        public LabelOutsidePlotAreaStyle AllowOutsidePlotArea { get; set; }
        public Boolean IsOverlappedHidden { get; set; }
        public Boolean IsMarkerOverlappingAllowed { get; set; }
        public LabelAlignmentStyles MovingDirection { get; set; }
    }

}