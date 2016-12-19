using NDataflash.Definition;
using NDataflashEditor.Storages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace NDataflashEditor.UI.PropertyGrid
{
    public class DefinedValueDropDownEditor : UITypeEditor
    {
        private IWindowsFormsEditorService _editorService;

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
           
            _editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            var descriptor = (Managers.DataflashPropertyDescriptor)context.PropertyDescriptor;

            // use a list box
            ListBox lb = new ListBox();
            lb.SelectionMode = SelectionMode.One;
            lb.SelectedValueChanged += OnListBoxSelectedValueChanged;
            lb.DisplayMember = nameof(PredefinedPropertyValueDefinition.Id);
            lb.ValueMember = nameof(PredefinedPropertyValueDefinition.ValueString);
            foreach (var val in descriptor.DataPropertyDefinition.PredefinedValues)
            {
                var index = lb.Items.Add(val);
                var typedValue = _Convert(val.ValueString, descriptor.PropertyType);
                if (typedValue.Equals(value))
                {
                    lb.SelectedIndex = index;
                }
            }

            //// show this model stuff
            _editorService.DropDownControl(lb);
            if (lb.SelectedItem == null) // no selection, return the passed-in value as is
                return value;
            var definedVAlue = (PredefinedPropertyValueDefinition)lb.SelectedItem;
            return _Convert(definedVAlue.ValueString, descriptor.PropertyType);
        }

        private object _Convert(String valueString, Type valueType)
        {
            if(valueType == typeof(uint))
            {
                return Int32.Parse(valueString);
            }
            if (valueType == typeof(ushort))
            {
                return Int16.Parse(valueString);
            }
            if (valueType == typeof(byte))
            {
                return Byte.Parse(valueString);
            }
            if (valueType == typeof(Boolean))
            {
                return Boolean.Parse(valueString);
            }
            return null;
        }

        private void OnListBoxSelectedValueChanged(object sender, EventArgs e)
        {
            // close the drop down as soon as something is clicked
            _editorService.CloseDropDown();
        }
    }
}
