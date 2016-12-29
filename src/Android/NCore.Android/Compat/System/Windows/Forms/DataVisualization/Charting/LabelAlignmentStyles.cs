using System;

namespace System.Windows.Forms.DataVisualization.Charting
{
    /// <summary>Specifies a label alignment when <see cref="T:System.Windows.Forms.DataVisualization.Charting.SmartLabelStyle" /> is used.</summary>
    [Flags]
    public enum LabelAlignmentStyles
    {
        /// <summary>Label is aligned to the top of the data point.</summary>
        Top = 1,
        /// <summary>Label is aligned to the bottom of the data point.</summary>
        Bottom = 2,
        /// <summary>Label is aligned to the right of the data point.</summary>
        Right = 4,
        /// <summary>Label is aligned to the left of the data point.</summary>
        Left = 8,
        /// <summary>Label is aligned to the top-left corner of the data point.</summary>
        TopLeft = 16,
        /// <summary>Label is aligned to the top-right corner of the data point.</summary>
        TopRight = 32,
        /// <summary>Label is aligned to the bottom-left of the data point.</summary>
        BottomLeft = 64,
        /// <summary>Label is aligned to the bottom-right of the data point.</summary>
        BottomRight = 128,
        /// <summary>Label is aligned to the center of the data point.</summary>
        Center = 256
    }
}