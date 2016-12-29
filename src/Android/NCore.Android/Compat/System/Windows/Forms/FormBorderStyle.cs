using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    /// <summary>Specifies the border styles for a form.</summary>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    public enum FormBorderStyle
    {
        /// <summary>No border.</summary>
        None,
        /// <summary>A fixed, single-line border.</summary>
        FixedSingle,
        /// <summary>A fixed, three-dimensional border.</summary>
        Fixed3D,
        /// <summary>A thick, fixed dialog-style border.</summary>
        FixedDialog,
        /// <summary>A resizable border.</summary>
        Sizable,
        /// <summary>A tool window border that is not resizable. A tool window does not appear in the taskbar or in the window that appears when the user presses ALT+TAB. Although forms that specify <see cref="F:System.Windows.Forms.FormBorderStyle.FixedToolWindow" /> typically are not shown in the taskbar, you must also ensure that the <see cref="P:System.Windows.Forms.Form.ShowInTaskbar" /> property is set to false, since its default value is true.</summary>
        FixedToolWindow,
        /// <summary>A resizable tool window border. A tool window does not appear in the taskbar or in the window that appears when the user presses ALT+TAB.</summary>
        SizableToolWindow
    }
}