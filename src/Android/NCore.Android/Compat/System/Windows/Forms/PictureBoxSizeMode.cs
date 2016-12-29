using System;

namespace System.Windows.Forms
{
    /// <summary>Specifies how an image is positioned within a <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
    /// <filterpriority>2</filterpriority>
    public enum PictureBoxSizeMode
    {
        /// <summary>The image is placed in the upper-left corner of the <see cref="T:System.Windows.Forms.PictureBox" />. The image is clipped if it is larger than the <see cref="T:System.Windows.Forms.PictureBox" /> it is contained in.</summary>
        Normal,
        /// <summary>The image within the <see cref="T:System.Windows.Forms.PictureBox" /> is stretched or shrunk to fit the size of the <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
        StretchImage,
        /// <summary>The <see cref="T:System.Windows.Forms.PictureBox" /> is sized equal to the size of the image that it contains.</summary>
        AutoSize,
        /// <summary>The image is displayed in the center if the <see cref="T:System.Windows.Forms.PictureBox" /> is larger than the image. If the image is larger than the <see cref="T:System.Windows.Forms.PictureBox" />, the picture is placed in the center of the <see cref="T:System.Windows.Forms.PictureBox" /> and the outside edges are clipped.</summary>
        CenterImage,
        /// <summary>The size of the image is increased or decreased maintaining the size ratio.</summary>
        Zoom
    }
}