using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    /// <summary>Represents the method that will handle the MouseDown, MouseUp, or MouseMove event of a form, control, or other component.</summary>
    /// <param name="sender">The source of the event. </param>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
    /// <filterpriority>2</filterpriority>
    public delegate void MouseEventHandler(object sender, MouseEventArgs e);

    /// <summary>Specifies constants that define which mouse button was pressed.</summary>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    [Flags]
    public enum MouseButtons
    {
        /// <summary>No mouse button was pressed.</summary>
        None = 0,
        /// <summary>The left mouse button was pressed.</summary>
        Left = 1048576,
        /// <summary>The right mouse button was pressed.</summary>
        Right = 2097152,
        /// <summary>The middle mouse button was pressed.</summary>
        Middle = 4194304,
        /// <summary>The first XButton was pressed.</summary>
        XButton1 = 8388608,
        /// <summary>The second XButton was pressed.</summary>
        XButton2 = 16777216
    }
}