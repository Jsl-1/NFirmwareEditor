using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    /// <summary>Specifies the type of action used to raise the <see cref="E:System.Windows.Forms.ScrollBar.Scroll" /> event.</summary>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    public enum ScrollEventType
    {
        /// <summary>The scroll box was moved a small distance. The user clicked the left(horizontal) or top(vertical) scroll arrow, or pressed the UP ARROW key.</summary>
        SmallDecrement,
        /// <summary>The scroll box was moved a small distance. The user clicked the right(horizontal) or bottom(vertical) scroll arrow, or pressed the DOWN ARROW key.</summary>
        SmallIncrement,
        /// <summary>The scroll box moved a large distance. The user clicked the scroll bar to the left(horizontal) or above(vertical) the scroll box, or pressed the PAGE UP key.</summary>
        LargeDecrement,
        /// <summary>The scroll box moved a large distance. The user clicked the scroll bar to the right(horizontal) or below(vertical) the scroll box, or pressed the PAGE DOWN key.</summary>
        LargeIncrement,
        /// <summary>The scroll box was moved.</summary>
        ThumbPosition,
        /// <summary>The scroll box is currently being moved.</summary>
        ThumbTrack,
        /// <summary>The scroll box was moved to the <see cref="P:System.Windows.Forms.ScrollBar.Minimum" /> position.</summary>
        First,
        /// <summary>The scroll box was moved to the <see cref="P:System.Windows.Forms.ScrollBar.Maximum" /> position.</summary>
        Last,
        /// <summary>The scroll box has stopped moving.</summary>
        EndScroll
    }
}