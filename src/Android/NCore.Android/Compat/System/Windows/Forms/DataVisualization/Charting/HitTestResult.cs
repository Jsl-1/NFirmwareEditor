using System;

namespace System.Windows.Forms.DataVisualization.Charting
{
    /// <summary>Represents the results of a hit test.</summary>
    public class HitTestResult
    {
        private object _obj;

        private Series _series;

        private int _dataPoint = -1;

        private ChartArea _chartArea;

        private Axis _axis;

        private ChartElementType _type;

        private object _subObject;

        /// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataVisualization.Charting.Axis" /> object that may be associated with a chart element returned by a hit test. The presence of an associated <see cref="T:System.Windows.Forms.DataVisualization.Charting.Axis" /> object depends on whether a chart element was detected at the given location, and the type of chart element returned.</summary>
        /// <returns>An <see cref="T:System.Windows.Forms.DataVisualization.Charting.Axis" /> object, otherwise null. The default value is null.</returns>
        public Axis Axis
        {
            get
            {
                return this._axis;
            }
            set
            {
                this._axis = value;
            }
        }

        /// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataVisualization.Charting.ChartArea" /> object that may be associated with a chart element returned by a hit test. The presence of a <see cref="T:System.Windows.Forms.DataVisualization.Charting.ChartArea" /> object depends on whether a chart element was detected at the given location, and the type of chart element returned.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.DataVisualization.Charting.ChartArea" /> object, otherwise null. The default value is null.</returns>
        public ChartArea ChartArea
        {
            get
            {
                return this._chartArea;
            }
            set
            {
                this._chartArea = value;
            }
        }

        /// <summary>Gets or sets the type of chart element, if any, returned by a hit test.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.DataVisualization.Charting.ChartElementType" /> enumeration value that indicates the type of chart element returned by a hit test. Defaults to <see cref="F:System.Windows.Forms.DataVisualization.Charting.ChartElementType.Nothing" />.</returns>
        public ChartElementType ChartElementType
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        /// <summary>Gets or sets the object, if any, returned by a hit test.</summary>
        /// <returns>An object of type <see cref="T:System.Object" />, otherwise null.</returns>
        public object Object
        {
            get
            {
                return this._obj;
            }
            set
            {
                this._obj = value;
            }
        }

        /// <summary>Gets or sets the index of a data point, if any, returned by a hit test.</summary>
        /// <returns>An integer value that represents the index of a data point returned by a hit test. The default value is negative one (-1).</returns>
        public int PointIndex
        {
            get
            {
                return this._dataPoint;
            }
            set
            {
                this._dataPoint = value;
            }
        }

        /// <summary>Gets or sets the associated series of a data point, if any, returned by a hit test.</summary>
        /// <returns>The <see cref="T:System.Windows.Forms.DataVisualization.Charting.Series" /> that a data point returned by a hit test belongs to. The default value is null.</returns>
        public Series Series
        {
            get
            {
                return this._series;
            }
            set
            {
                this._series = value;
            }
        }

        /// <summary>Gets the sub-object, if any, returned by a hit test.</summary>
        /// <returns>A sub-object of type <see cref="T:System.Object" />, otherwise null.</returns>
        public object SubObject
        {
            get
            {
                return this._subObject;
            }
            set
            {
                this._subObject = value;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataVisualization.Charting.HitTestResult" /> class.</summary>
        public HitTestResult()
        {
        }
    }
}