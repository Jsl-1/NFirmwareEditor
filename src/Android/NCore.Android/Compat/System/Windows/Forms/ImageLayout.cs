using System;

namespace System.Windows.Forms
{
    /// <summary>Specifies the position of the image on the control.</summary>
    /// <filterpriority>2</filterpriority>
    public enum ImageLayout
    {
        /// <summary>The image is left-aligned at the top across the control's client rectangle.</summary>
        None,
        /// <summary>The image is tiled across the control's client rectangle.</summary>
        Tile,
        /// <summary>The image is centered within the control's client rectangle.</summary>
        Center,
        /// <summary>The image is streched across the control's client rectangle.</summary>
        Stretch,
        /// <summary>The image is enlarged within the control's client rectangle.</summary>
        Zoom
    }
}