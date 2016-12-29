using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
    /// <summary>Specifies how a control anchors to the edges of its container.</summary>
    /// <filterpriority>2</filterpriority>
    [Flags]
    public enum AnchorStyles
    {
        /// <summary>The control is not anchored to any edges of its container.</summary>
        None = 0,
        /// <summary>The control is anchored to the top edge of its container.</summary>
        Top = 1,
        /// <summary>The control is anchored to the bottom edge of its container.</summary>
        Bottom = 2,
        /// <summary>The control is anchored to the left edge of its container.</summary>
        Left = 4,
        /// <summary>The control is anchored to the right edge of its container.</summary>
        Right = 8
    }
}