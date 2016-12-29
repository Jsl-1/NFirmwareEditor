using System;

namespace System.Windows.Forms.DataVisualization.Charting
{
    /// <summary>Specifies a palette when setting a Palette property. </summary>
    public enum ChartColorPalette
    {
        /// <summary>No palette is used. </summary>
        None,
        /// <summary>Palette utilizes bright colors.</summary>
        Bright,
        /// <summary>Palette utilizes grayscale colors, that is, shades of black and white.</summary>
        Grayscale,
        /// <summary>Palette utilizes Excel-style colors.</summary>
        Excel,
        /// <summary>Palette utilizes <see cref="T:System.Web.UI.DataVisualization.Charting.LightStyle" /> style colors; very light colors..</summary>
        Light,
        /// <summary>Palette utilizes pastel colors.</summary>
        Pastel,
        /// <summary>Palette utilizes earth tone colors such as green and brown.</summary>
        EarthTones,
        /// <summary>Palette utilizes semi-transparent colors.</summary>
        SemiTransparent,
        /// <summary>Palette utilizes blues and purples.</summary>
        Berry,
        /// <summary>Palette utilizes shades of brown.</summary>
        Chocolate,
        /// <summary>Palette utilizes red, orange and yellow colors.</summary>
        Fire,
        /// <summary>Palette utilizes colors that range from green to blue.</summary>
        SeaGreen,
        /// <summary>Palette utilizes bright pastel colors.</summary>
        BrightPastel
    }
}