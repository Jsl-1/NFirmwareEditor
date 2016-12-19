using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NDataflashEditor.Classes
{
    public class CustomValuePropertyDescriptor : PropertyDescriptor
    {
        object m_value;

        public CustomValuePropertyDescriptor(string propertyName, object value) : base(propertyName, new Attribute[0]) 
        {
            m_value = value;
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
                return true;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return m_value.GetType();
            }
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override object GetValue(object component)
        {
            return m_value;
        }

        public override void ResetValue(object component)
        {
            
        }

        public override void SetValue(object component, object value)
        {
           
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
    }
}
