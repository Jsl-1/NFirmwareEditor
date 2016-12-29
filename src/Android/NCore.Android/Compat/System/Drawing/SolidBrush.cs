using System.Drawing;

namespace System.Drawing
{
    public class SolidBrush : Brush
    {
        private Color headerBackColor;

        public SolidBrush(Color headerBackColor)
        {
            this.headerBackColor = headerBackColor;
        }
    }
}