using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    /// <summary>Specifies the border style for a control.</summary>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    public enum BorderStyle
    {
        /// <summary>No border.</summary>
        None,
        /// <summary>A single-line border.</summary>
        FixedSingle,
        /// <summary>A three-dimensional border.</summary>
        Fixed3D
    }
}