using System;

namespace System.Windows.Forms
{
    /// <summary>Specifies the different types of automatic scaling modes supported by Windows Forms.</summary>
    public enum AutoScaleMode
    {
        /// <summary>Automatic scaling is disabled.</summary>
        None,
        /// <summary>Controls scale relative to the dimensions of the font the classes are using, which is typically the system font.</summary>
        Font,
        /// <summary>Controls scale relative to the display resolution. Common resolutions are 96 and 120 DPI.</summary>
        Dpi,
        /// <summary>Controls scale according to the classes' parent's scaling mode. If there is no parent, automatic scaling is disabled.</summary>
        Inherit
    }
}