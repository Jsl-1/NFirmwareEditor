using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Collections.ObjectModel;

namespace System.Windows.Forms
{
    public class ContextMenu
    {
        private MenuItem[] menuItem;

        public MenuItemCollection MenuItems { get; } = new MenuItemCollection();

        public ContextMenu()
        {

        }

        public ContextMenu(MenuItem[] menuItem)
        {
            this.menuItem = menuItem;
        }


        public void Show(Control configurationMenuButton, Point point)
        {
            throw new NotImplementedException();
        }
    }

    public class MenuItem
    {
        private Action<object, EventArgs> newMenuItem_Click;
        private string v;

        public MenuItem(string v, Action<object, EventArgs> newMenuItem_Click)
        {
            this.v = v;
            this.newMenuItem_Click = newMenuItem_Click;
        }
    }

    public class MenuItemCollection : Collection<MenuItem>
    {
        //public void Add(string caption, )
        public void Add(string itemCaption, EventHandler handler)
        {
           // throw new NotImplementedException();
        }
    }
}