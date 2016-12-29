using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace System.Windows.Forms
{
    public class ComboBox : Control
    {
        public event EventHandler SelectedValueChanged;

        public object SelectedItem { get; set; }

        public List<Object> Items { get; } = new List<object>();
        public ComboBoxStyle DropDownStyle { get; internal set; }
        public bool FormattingEnabled { get; internal set; }
        public int SelectedIndex { get; internal set; }


        public void SelectItem(object selectedProfile)
        {
            SelectedItem = selectedProfile;
        }

        public T GetSelectedItem<T>()
        {
            if (SelectedItem == null)
                return default(T);
            return (T)SelectedItem;
        }

    }
}