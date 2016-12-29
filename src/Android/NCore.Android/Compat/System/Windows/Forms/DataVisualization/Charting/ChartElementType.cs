using System;

namespace System.Windows.Forms.DataVisualization.Charting
{
    /// <summary>Specifies a chart element type.</summary>
    public enum ChartElementType
    {
        /// <summary>No chart element.</summary>
        Nothing,
        /// <summary>The title of a <see cref="T:System.Windows.Forms.DataVisualization.Charting.Chart" />.</summary>
        Title,
        /// <summary>The plotting area, which is the chart area excluding axes, labels, and so on. Also excludes the areas that data points might occupy.</summary>
        PlottingArea,
        /// <summary>An <see cref="T:System.Windows.Forms.DataVisualization.Charting.Axis" /> object.</summary>
        Axis,
        /// <summary>Any major or minor <see cref="T:System.Windows.Forms.DataVisualization.Charting.TickMark" /> object.</summary>
        TickMarks,
        /// <summary>Any major or minor grid line, either vertical or horizontal.</summary>
        Gridlines,
        /// <summary>A <see cref="T:System.Windows.Forms.DataVisualization.Charting.StripLine" /> object.</summary>
        StripLines,
        /// <summary>An <see cref="T:System.Windows.Forms.DataVisualization.Charting.Axis" /> label image.</summary>
        AxisLabelImage,
        /// <summary>An <see cref="T:System.Web.UI.DataVisualization.Charting.Axis" /> label.</summary>
        AxisLabels,
        /// <summary>An <see cref="T:System.Windows.Forms.DataVisualization.Charting.Axis" /> title.</summary>
        AxisTitle,
        /// <summary>A scrollbar tracking thumb.</summary>
        ScrollBarThumbTracker,
        /// <summary>A scrollbar small decrement button. A "down arrow" button for a vertical scrollbar, a "left arrow" button for a horizontal scrollbar.</summary>
        ScrollBarSmallDecrement,
        /// <summary>A scrollbar small increment button. An "up arrow" button for a vertical scrollbar, a "right arrow" button for a horizontal scrollbar.</summary>
        ScrollBarSmallIncrement,
        /// <summary>The background of a scrollbar that, when clicked, results in a large decrement in the view size. This background is located below the thumb for a vertical scrollbar, and to the left of the thumb for a horizontal scrollbar.</summary>
        ScrollBarLargeDecrement,
        /// <summary>The background of a scrollbar that, when clicked, results in a large increment in the view size. This background is located above the thumb for a vertical scrollbar, and to the right of the thumb for a horizontal scrollbar.</summary>
        ScrollBarLargeIncrement,
        /// <summary>The <see cref="F:System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonType.ZoomReset" /> button of a scrollbar.</summary>
        ScrollBarZoomReset,
        /// <summary>A <see cref="T:System.Windows.Forms.DataVisualization.Charting.DataPoint" /> object.</summary>
        DataPoint,
        /// <summary>A series <see cref="T:System.Windows.Forms.DataVisualization.Charting.DataPoint" /> label.</summary>
        DataPointLabel,
        /// <summary>The area inside a <see cref="T:System.Windows.Forms.DataVisualization.Charting.Legend" /> object. Does not include the space occupied by legend items.</summary>
        LegendArea,
        /// <summary>A <see cref="T:System.Windows.Forms.DataVisualization.Charting.Legend" /> title.</summary>
        LegendTitle,
        /// <summary>A <see cref="T:System.Windows.Forms.DataVisualization.Charting.Legend" /> header.</summary>
        LegendHeader,
        /// <summary>A <see cref="T:System.Windows.Forms.DataVisualization.Charting.LegendItem" /> object.</summary>
        LegendItem,
        /// <summary>Any object derived from the <see cref="T:System.Windows.Forms.DataVisualization.Charting.Annotation" /> class.</summary>
        Annotation
    }
}