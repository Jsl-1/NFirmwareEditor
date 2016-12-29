using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Windows.Forms.DataVisualization.Charting
{
    /// <summary>Represents the base class for all chart element collections.</summary>
    /// <typeparam name="T"></typeparam>
    public class SeriesCollection : Collection<Series>
    {
        public Series this[string seriesName]
        {
            get { return this.Items.FirstOrDefault( x => x.Name == seriesName); }
        }
    }
}