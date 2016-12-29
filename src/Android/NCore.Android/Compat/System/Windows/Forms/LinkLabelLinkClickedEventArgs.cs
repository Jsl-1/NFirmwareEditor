using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public delegate void LinkClickedEventHandler(object sender, LinkClickedEventArgs e);
    public delegate void LinkLabelLinkClickedEventHandler(object sender, LinkLabelLinkClickedEventArgs e);

    /// <summary>Provides data for the <see cref="E:System.Windows.Forms.RichTextBox.LinkClicked" /> event.</summary>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    public class LinkClickedEventArgs : EventArgs
    {
        private string linkText;

        /// <summary>Gets the text of the link being clicked.</summary>
        /// <returns>The text of the link that is clicked in the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</returns>
        /// <filterpriority>1</filterpriority>
        public string LinkText
        {
            get
            {
                return this.linkText;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkClickedEventArgs" /> class.</summary>
        /// <param name="linkText">The text of the link that is clicked in the <see cref="T:System.Windows.Forms.RichTextBox" /> control. </param>
        public LinkClickedEventArgs(string linkText)
        {
            this.linkText = linkText;
        }
    }

     /// <summary>Provides data for the <see cref="E:System.Windows.Forms.LinkLabel.LinkClicked" /> event.</summary>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    public class LinkLabelLinkClickedEventArgs : EventArgs
    {
        private readonly LinkLabel.Link link;

        private readonly MouseButtons button;

        /// <summary>Gets the mouse button that causes the link to be clicked.</summary>
        /// <returns>One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values.</returns>
        public MouseButtons Button
        {
            get
            {
                return this.button;
            }
        }

        /// <summary>Gets the <see cref="T:System.Windows.Forms.LinkLabel.Link" /> that was clicked.</summary>
        /// <returns>A link on the <see cref="T:System.Windows.Forms.LinkLabel" />.</returns>
        /// <filterpriority>1</filterpriority>
        public LinkLabel.Link Link
        {
            get
            {
                return this.link;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs" /> class with the specified link.</summary>
        /// <param name="link">The <see cref="T:System.Windows.Forms.LinkLabel.Link" /> that was clicked. </param>
        public LinkLabelLinkClickedEventArgs(LinkLabel.Link link)
        {
            this.link = link;
            this.button = MouseButtons.Left;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs" /> class with the specified link and the specified mouse button.</summary>
        /// <param name="link">The <see cref="T:System.Windows.Forms.LinkLabel.Link" /> that was clicked. </param>
        /// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values.</param>
        public LinkLabelLinkClickedEventArgs(LinkLabel.Link link, MouseButtons button) : this(link)
        {
            this.button = button;
        }
    }
}