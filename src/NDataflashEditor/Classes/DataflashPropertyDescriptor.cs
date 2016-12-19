using NDataflash;
using NDataflash.Definition;
using NDataflashEditor.Storages;
using NDataflashEditor.UI.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;

namespace NDataflashEditor.Managers
{
    internal class DataflashPropertyDescriptor : PropertyDescriptor
    {
        private DataflashManager m_genericDataflashManager;
        private PropertyDefinition m_dataPropertyDefinition;
        private string m_DisplayName;

        internal DataflashPropertyDescriptor(DataflashManager genericDataflashManager, PropertyDefinition property, Attribute[] attrs)
            : this(genericDataflashManager, property, attrs, property.Id)
        {

        }

        internal DataflashPropertyDescriptor(DataflashManager genericDataflashManager, PropertyDefinition property, Attribute[] attrs, String displayName) : base(displayName, attrs)
        {
            this.m_genericDataflashManager = genericDataflashManager;
            this.m_dataPropertyDefinition = property;
            m_DisplayName = displayName;
            var attributeList = new List<Attribute>(AttributeArray);
            if (m_dataPropertyDefinition.PredefinedValues != null && m_dataPropertyDefinition.PredefinedValues.Count > 0)
            {
                attributeList.Add(new EditorAttribute(typeof(DefinedValueDropDownEditor), typeof(UITypeEditor)));
            }
            AttributeArray = attributeList.ToArray();
        }

        public PropertyDefinition DataPropertyDefinition
        {
            get { return m_dataPropertyDefinition; }
        }

        public override string Name
        {
            get
            {
                return m_DisplayName;
            }
        }

        public override Type ComponentType
        {
            get
            {
                return null;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return m_dataPropertyDefinition.ReadOnly;
            }
        }

        public override Type PropertyType
        {
            get
            {

                return m_dataPropertyDefinition.GetPropertyType();
            }
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override object GetValue(object component)
        {
            return m_genericDataflashManager.GetPropertyValue(m_dataPropertyDefinition);            
        }

        public override void ResetValue(object component)
        {
            var originalValue = m_genericDataflashManager.GetInitialPropertyValue(m_dataPropertyDefinition);
            SetValue(component, originalValue);
        }

        public override void SetValue(object component, object value)
        {
            m_genericDataflashManager.SetPropertyValue(m_dataPropertyDefinition, value);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
    }
}
