namespace System.Drawing
{
    public class Font
    {
        private GraphicsUnit point;
        private FontStyle regular;
        private string v1;
        private float v2;
        private byte v3;

        public Font(string v1, float v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public Font(string v1, float v2, FontStyle regular, GraphicsUnit point, byte v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.regular = regular;
            this.point = point;
            this.v3 = v3;
        }
    }
}