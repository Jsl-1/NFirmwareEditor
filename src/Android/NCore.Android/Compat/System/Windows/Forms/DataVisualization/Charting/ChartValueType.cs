using System;

namespace System.Windows.Forms.DataVisualization.Charting
{
    /// <summary>Specifies value types for various chart properties.</summary>
    public enum ChartValueType
    {
        /// <summary>Property type is set automatically by the Chart control.</summary>
        Auto,
        /// <summary>A <see cref="T:System.Double" /> value.</summary>
        Double,
        /// <summary>A <see cref="T:System.Single" /> value.</summary>
        Single,
        /// <summary>A <see cref="T:System.Int32" /> value.</summary>
        Int32,
        /// <summary>A <see cref="T:System.Int64" /> value.</summary>
        Int64,
        /// <summary>A <see cref="T:System.UInt32" /> value.</summary>
        UInt32,
        /// <summary>A <see cref="T:System.UInt64" /> value.</summary>
        UInt64,
        /// <summary>A <see cref="T:System.String" /> value.</summary>
        String,
        /// <summary>A <see cref="T:System.DateTime" /> value.</summary>
        DateTime,
        /// <summary>The Date portion of a <see cref="T:System.DateTime" /> value.</summary>
        Date,
        /// <summary>The Time portion of the <see cref="DateTime" /> value.</summary>
        Time,
        /// <summary>A <see cref="T:System.DateTime" /> value with offset.</summary>
        DateTimeOffset
    }
}