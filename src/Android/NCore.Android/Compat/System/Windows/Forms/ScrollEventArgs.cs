using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    /// <summary>Provides data for the Scroll event.</summary>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    public class ScrollEventArgs : EventArgs
    {
        private readonly ScrollEventType type;

        private int newValue;

        private ScrollOrientation scrollOrientation;

        private int oldValue = -1;

        /// <summary>Gets or sets the new <see cref="P:System.Windows.Forms.ScrollBar.Value" /> of the scroll bar.</summary>
        /// <returns>The numeric value that the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property will be changed to.</returns>
        /// <filterpriority>1</filterpriority>
        public int NewValue
        {
            get
            {
                return this.newValue;
            }
            set
            {
                this.newValue = value;
            }
        }

        /// <summary>Gets the old <see cref="P:System.Windows.Forms.ScrollBar.Value" /> of the scroll bar.</summary>
        /// <returns>The numeric value that the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property contained before it changed.</returns>
        /// <filterpriority>1</filterpriority>
        public int OldValue
        {
            get
            {
                return this.oldValue;
            }
        }

        /// <summary>Gets the scroll bar orientation that raised the Scroll event.</summary>
        /// <returns>One of the <see cref="T:System.Windows.Forms.ScrollOrientation" /> values.</returns>
        /// <filterpriority>1</filterpriority>
        public ScrollOrientation ScrollOrientation
        {
            get
            {
                return this.scrollOrientation;
            }
        }

        /// <summary>Gets the type of scroll event that occurred.</summary>
        /// <returns>One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values.</returns>
        /// <filterpriority>1</filterpriority>
        public ScrollEventType Type
        {
            get
            {
                return this.type;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" /> and <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" /> properties.</summary>
        /// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values. </param>
        /// <param name="newValue">The new value for the scroll bar. </param>
        public ScrollEventArgs(ScrollEventType type, int newValue)
        {
            this.type = type;
            this.newValue = newValue;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" />, and <see cref="P:System.Windows.Forms.ScrollEventArgs.ScrollOrientation" /> properties.</summary>
        /// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values. </param>
        /// <param name="newValue">The new value for the scroll bar. </param>
        /// <param name="scroll">One of the <see cref="T:System.Windows.Forms.ScrollOrientation" /> values. </param>
        public ScrollEventArgs(ScrollEventType type, int newValue, ScrollOrientation scroll)
        {
            this.type = type;
            this.newValue = newValue;
            this.scrollOrientation = scroll;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.OldValue" />, and <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" /> properties.</summary>
        /// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values. </param>
        /// <param name="oldValue">The old value for the scroll bar. </param>
        /// <param name="newValue">The new value for the scroll bar. </param>
        public ScrollEventArgs(ScrollEventType type, int oldValue, int newValue)
        {
            this.type = type;
            this.newValue = newValue;
            this.oldValue = oldValue;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.OldValue" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" />, and <see cref="P:System.Windows.Forms.ScrollEventArgs.ScrollOrientation" /> properties.</summary>
        /// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values. </param>
        /// <param name="oldValue">The old value for the scroll bar. </param>
        /// <param name="newValue">The new value for the scroll bar. </param>
        /// <param name="scroll">One of the <see cref="T:System.Windows.Forms.ScrollOrientation" /> values. </param>
        public ScrollEventArgs(ScrollEventType type, int oldValue, int newValue, ScrollOrientation scroll)
        {
            this.type = type;
            this.newValue = newValue;
            this.scrollOrientation = scroll;
            this.oldValue = oldValue;
        }
    }
}