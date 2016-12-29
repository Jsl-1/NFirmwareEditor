using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    /// <summary>Specifies the initial position of a form.</summary>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    public enum FormStartPosition
    {
        /// <summary>The position of the form is determined by the <see cref="P:System.Windows.Forms.Control.Location" /> property.</summary>
        Manual,
        /// <summary>The form is centered on the current display, and has the dimensions specified in the form's size.</summary>
        CenterScreen,
        /// <summary>The form is positioned at the Windows default location and has the dimensions specified in the form's size.</summary>
        WindowsDefaultLocation,
        /// <summary>The form is positioned at the Windows default location and has the bounds determined by Windows default.</summary>
        WindowsDefaultBounds,
        /// <summary>The form is centered within the bounds of its parent form.</summary>
        CenterParent
    }
}