using System.Drawing;

namespace System.Drawing
{
    public class Pen : IDisposable
    {
        private Color borderColor;
        private float v;

        public Pen(Color borderColor, int v)
        {
            this.borderColor = borderColor;
            this.v = v;
        }

        public Pen(Color borderColor, float v)
        {
            this.borderColor = borderColor;
            this.v = v;
        }

        public void Dispose()
        {

        }
    }
}