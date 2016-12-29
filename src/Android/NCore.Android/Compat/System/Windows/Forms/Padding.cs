using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms
{
    /// <summary>Represents padding or margin information associated with a user interface (UI) element.</summary>
    /// <filterpriority>2</filterpriority>
    [Serializable]
    [TypeConverter(typeof(PaddingConverter))]
    public struct Padding
    {
        private bool _all;

        private int _top;

        private int _left;

        private int _right;

        private int _bottom;

        /// <summary>Provides a <see cref="T:System.Windows.Forms.Padding" /> object with no padding.</summary>
        /// <filterpriority>1</filterpriority>
        public readonly static Padding Empty;

        /// <summary>Gets or sets the padding value for all the edges.</summary>
        /// <returns>The padding, in pixels, for all edges if the same; otherwise, -1.</returns>
        /// <filterpriority>1</filterpriority>
        [RefreshProperties(RefreshProperties.All)]
        public int All
        {
            get
            {
                if (!this._all)
                {
                    return -1;
                }
                return this._top;
            }
            set
            {
                if (!this._all || this._top != value)
                {
                    this._all = true;
                    int num = value;
                    int num1 = num;
                    this._bottom = num;
                    int num2 = num1;
                    num1 = num2;
                    this._right = num2;
                    int num3 = num1;
                    num1 = num3;
                    this._left = num3;
                    this._top = num1;
                }
            }
        }

        /// <summary>Gets or sets the padding value for the bottom edge.</summary>
        /// <returns>The padding, in pixels, for the bottom edge.</returns>
        /// <filterpriority>1</filterpriority>
        [RefreshProperties(RefreshProperties.All)]
        public int Bottom
        {
            get
            {
                if (this._all)
                {
                    return this._top;
                }
                return this._bottom;
            }
            set
            {
                if (this._all || this._bottom != value)
                {
                    this._all = false;
                    this._bottom = value;
                }
            }
        }

        /// <summary>Gets the combined padding for the right and left edges.</summary>
        /// <returns>Gets the sum, in pixels, of the <see cref="P:System.Windows.Forms.Padding.Left" /> and <see cref="P:System.Windows.Forms.Padding.Right" /> padding values.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public int Horizontal
        {
            get
            {
                return this.Left + this.Right;
            }
        }

        /// <summary>Gets or sets the padding value for the left edge.</summary>
        /// <returns>The padding, in pixels, for the left edge.</returns>
        /// <filterpriority>1</filterpriority>
        [RefreshProperties(RefreshProperties.All)]
        public int Left
        {
            get
            {
                if (this._all)
                {
                    return this._top;
                }
                return this._left;
            }
            set
            {
                if (this._all || this._left != value)
                {
                    this._all = false;
                    this._left = value;
                }
            }
        }

        /// <summary>Gets or sets the padding value for the right edge.</summary>
        /// <returns>The padding, in pixels, for the right edge.</returns>
        /// <filterpriority>1</filterpriority>
        [RefreshProperties(RefreshProperties.All)]
        public int Right
        {
            get
            {
                if (this._all)
                {
                    return this._top;
                }
                return this._right;
            }
            set
            {
                if (this._all || this._right != value)
                {
                    this._all = false;
                    this._right = value;
                }
            }
        }

        /// <summary>Gets the padding information in the form of a <see cref="T:System.Drawing.Size" />.</summary>
        /// <returns>A <see cref="T:System.Drawing.Size" /> containing the padding information.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public Size Size
        {
            get
            {
                return new Size(this.Horizontal, this.Vertical);
            }
        }

        /// <summary>Gets or sets the padding value for the top edge.</summary>
        /// <returns>The padding, in pixels, for the top edge.</returns>
        /// <filterpriority>1</filterpriority>
        [RefreshProperties(RefreshProperties.All)]
        public int Top
        {
            get
            {
                return this._top;
            }
            set
            {
                if (this._all || this._top != value)
                {
                    this._all = false;
                    this._top = value;
                }
            }
        }

        /// <summary>Gets the combined padding for the top and bottom edges.</summary>
        /// <returns>Gets the sum, in pixels, of the <see cref="P:System.Windows.Forms.Padding.Top" /> and <see cref="P:System.Windows.Forms.Padding.Bottom" /> padding values.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public int Vertical
        {
            get
            {
                return this.Top + this.Bottom;
            }
        }

        static Padding()
        {
            Padding.Empty = new Padding(0);
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Padding" /> class using the supplied padding size for all edges.</summary>
        /// <param name="all">The number of pixels to be used for padding for all edges.</param>
        public Padding(int all)
        {
            this._all = true;
            int num = all;
            int num1 = num;
            this._bottom = num;
            int num2 = num1;
            num1 = num2;
            this._right = num2;
            int num3 = num1;
            num1 = num3;
            this._left = num3;
            this._top = num1;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Padding" /> class using a separate padding size for each edge.</summary>
        /// <param name="left">The padding size, in pixels, for the left edge.</param>
        /// <param name="top">The padding size, in pixels, for the top edge.</param>
        /// <param name="right">The padding size, in pixels, for the right edge.</param>
        /// <param name="bottom">The padding size, in pixels, for the bottom edge.</param>
        public Padding(int left, int top, int right, int bottom)
        {
            this._top = top;
            this._left = left;
            this._right = right;
            this._bottom = bottom;
            this._all = (this._top != this._left || this._top != this._right ? false : this._top == this._bottom);
        }

        /// <summary>Computes the sum of the two specified <see cref="T:System.Windows.Forms.Padding" /> values.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that contains the sum of the two specified <see cref="T:System.Windows.Forms.Padding" /> values.</returns>
        /// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" />.</param>
        /// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" />.</param>
        public static Padding Add(Padding p1, Padding p2)
        {
            return p1 + p2;
        }

        [Conditional("DEBUG")]
        private void Debug_SanityCheck()
        {
            bool flag = this._all;
        }

        /// <summary>Determines whether the value of the specified object is equivalent to the current <see cref="T:System.Windows.Forms.Padding" />.</summary>
        /// <returns>true if the <see cref="T:System.Windows.Forms.Padding" /> objects are equivalent; otherwise, false.</returns>
        /// <param name="other">The object to compare to the current <see cref="T:System.Windows.Forms.Padding" />.</param>
        /// <filterpriority>1</filterpriority>
        public override bool Equals(object other)
        {
            if (!(other is Padding))
            {
                return false;
            }
            return (Padding)other == this;
        }

        /// <summary>Generates a hash code for the current <see cref="T:System.Windows.Forms.Padding" />. </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        /// <filterpriority>1</filterpriority>
        public override int GetHashCode()
        {
            return this.Left ^ WindowsFormsUtils.RotateLeft(this.Top, 8) ^ WindowsFormsUtils.RotateLeft(this.Right, 16) ^ WindowsFormsUtils.RotateLeft(this.Bottom, 24);
        }

        /// <summary>Performs vector addition on the two specified <see cref="T:System.Windows.Forms.Padding" /> objects, resulting in a new <see cref="T:System.Windows.Forms.Padding" />.</summary>
        /// <returns>A new <see cref="T:System.Windows.Forms.Padding" /> that results from adding <paramref name="p1" /> and <paramref name="p2" />.</returns>
        /// <param name="p1">The first <see cref="T:System.Windows.Forms.Padding" /> to add.</param>
        /// <param name="p2">The second <see cref="T:System.Windows.Forms.Padding" /> to add.</param>
        /// <filterpriority>3</filterpriority>
        public static Padding operator +(Padding p1, Padding p2)
        {
            return new Padding(p1.Left + p2.Left, p1.Top + p2.Top, p1.Right + p2.Right, p1.Bottom + p2.Bottom);
        }

        /// <summary>Tests whether two specified <see cref="T:System.Windows.Forms.Padding" /> objects are equivalent.</summary>
        /// <returns>true if the two <see cref="T:System.Windows.Forms.Padding" /> objects are equal; otherwise, false.</returns>
        /// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
        /// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
        /// <filterpriority>3</filterpriority>
        public static bool operator ==(Padding p1, Padding p2)
        {
            if (p1.Left != p2.Left || p1.Top != p2.Top || p1.Right != p2.Right)
            {
                return false;
            }
            return p1.Bottom == p2.Bottom;
        }

        /// <summary>Tests whether two specified <see cref="T:System.Windows.Forms.Padding" /> objects are not equivalent.</summary>
        /// <returns>true if the two <see cref="T:System.Windows.Forms.Padding" /> objects are different; otherwise, false.</returns>
        /// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
        /// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
        /// <filterpriority>3</filterpriority>
        public static bool operator !=(Padding p1, Padding p2)
        {
            return !(p1 == p2);
        }

        /// <summary>Performs vector subtraction on the two specified <see cref="T:System.Windows.Forms.Padding" /> objects, resulting in a new <see cref="T:System.Windows.Forms.Padding" />.</summary>
        /// <returns>The <see cref="T:System.Windows.Forms.Padding" /> result of subtracting <paramref name="p2" /> from <paramref name="p1" />.</returns>
        /// <param name="p1">The <see cref="T:System.Windows.Forms.Padding" /> to subtract from (the minuend).</param>
        /// <param name="p2">The <see cref="T:System.Windows.Forms.Padding" /> to subtract from (the subtrahend).</param>
        /// <filterpriority>3</filterpriority>
        public static Padding operator -(Padding p1, Padding p2)
        {
            return new Padding(p1.Left - p2.Left, p1.Top - p2.Top, p1.Right - p2.Right, p1.Bottom - p2.Bottom);
        }

        private void ResetAll()
        {
            this.All = 0;
        }

        private void ResetBottom()
        {
            this.Bottom = 0;
        }

        private void ResetLeft()
        {
            this.Left = 0;
        }

        private void ResetRight()
        {
            this.Right = 0;
        }

        private void ResetTop()
        {
            this.Top = 0;
        }

        internal void Scale(float dx, float dy)
        {
            this._top = (int)((float)this._top * dy);
            this._left = (int)((float)this._left * dx);
            this._right = (int)((float)this._right * dx);
            this._bottom = (int)((float)this._bottom * dy);
        }

        internal bool ShouldSerializeAll()
        {
            return this._all;
        }

        /// <summary>Subtracts one specified <see cref="T:System.Windows.Forms.Padding" /> value from another.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that contains the result of the subtraction of one specified <see cref="T:System.Windows.Forms.Padding" /> value from another.</returns>
        /// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" />.</param>
        /// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" />.</param>
        public static Padding Subtract(Padding p1, Padding p2)
        {
            return p1 - p2;
        }

        /// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.Padding" />.</summary>
        /// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Padding" />.</returns>
        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            string[] str = new string[] { "{Left=", null, null, null, null, null, null, null, null };
            int left = this.Left;
            str[1] = left.ToString(CultureInfo.CurrentCulture);
            str[2] = ",Top=";
            left = this.Top;
            str[3] = left.ToString(CultureInfo.CurrentCulture);
            str[4] = ",Right=";
            left = this.Right;
            str[5] = left.ToString(CultureInfo.CurrentCulture);
            str[6] = ",Bottom=";
            left = this.Bottom;
            str[7] = left.ToString(CultureInfo.CurrentCulture);
            str[8] = "}";
            return string.Concat(str);
        }
    }
}