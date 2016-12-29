using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    /// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.MouseUp" />, <see cref="E:System.Windows.Forms.Control.MouseDown" />, and <see cref="E:System.Windows.Forms.Control.MouseMove" /> events.</summary>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    public class MouseEventArgs : EventArgs
    {
        private readonly MouseButtons button;

        private readonly int clicks;

        private readonly int x;

        private readonly int y;

        private readonly int delta;

        /// <summary>Gets which mouse button was pressed.</summary>
        /// <returns>One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values.</returns>
        /// <filterpriority>1</filterpriority>
        public MouseButtons Button
        {
            get
            {
                return this.button;
            }
        }

        /// <summary>Gets the number of times the mouse button was pressed and released.</summary>
        /// <returns>An <see cref="T:System.Int32" /> that contains the number of times the mouse button was pressed and released.</returns>
        /// <filterpriority>1</filterpriority>
        public int Clicks
        {
            get
            {
                return this.clicks;
            }
        }

        /// <summary>Gets a signed count of the number of detents the mouse wheel has rotated, multiplied by the WHEEL_DELTA constant. A detent is one notch of the mouse wheel.</summary>
        /// <returns>A signed count of the number of detents the mouse wheel has rotated, multiplied by the WHEEL_DELTA constant.</returns>
        /// <filterpriority>1</filterpriority>
        public int Delta
        {
            get
            {
                return this.delta;
            }
        }

        /// <summary>Gets the location of the mouse during the generating mouse event.</summary>
        /// <returns>A <see cref="T:System.Drawing.Point" /> that contains the x- and y- mouse coordinates, in pixels, relative to the upper-left corner of the form.</returns>
        /// <filterpriority>1</filterpriority>
        public Point Location
        {
            get
            {
                return new Point(this.x, this.y);
            }
        }

        /// <summary>Gets the x-coordinate of the mouse during the generating mouse event.</summary>
        /// <returns>The x-coordinate of the mouse, in pixels.</returns>
        /// <filterpriority>1</filterpriority>
        public int X
        {
            get
            {
                return this.x;
            }
        }

        /// <summary>Gets the y-coordinate of the mouse during the generating mouse event.</summary>
        /// <returns>The y-coordinate of the mouse, in pixels.</returns>
        /// <filterpriority>1</filterpriority>
        public int Y
        {
            get
            {
                return this.y;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MouseEventArgs" /> class.</summary>
        /// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
        /// <param name="clicks">The number of times a mouse button was pressed. </param>
        /// <param name="x">The x-coordinate of a mouse click, in pixels. </param>
        /// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
        /// <param name="delta">A signed count of the number of detents the wheel has rotated. </param>
        public MouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
        {
            this.button = button;
            this.clicks = clicks;
            this.x = x;
            this.y = y;
            this.delta = delta;
        }
    }
}