using NDataflash.Definition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NDataflash.Descriptor
{
    public class DataflashPropertyDescriptor : PropertyDescriptor
    {
        private DataflashObjectDescriptor m_ParentObjectDescriptor;

        public DataflashPropertyDescriptor(DataflashObjectDescriptor parentObjectDescriptor, string propertyName) : base(propertyName, new Attribute[0] )
        {
            m_ParentObjectDescriptor = parentObjectDescriptor;
        }

        #region PropertyDescriptor Members

        public override Type ComponentType { get { return null; } }
       

        public override bool IsReadOnly { get { return m_ParentObjectDescriptor.Definition.ReadOnly; } }


        public override Type PropertyType
        {
            get
            {
                if (m_ParentObjectDescriptor.Definition.IsArray )
                {
                    return typeof(DataflashObjectDescriptor[]);
                }
                else if(m_ParentObjectDescriptor.Definition is ObjectPropertyDefinition)
                {
                    return typeof(DataflashObjectDescriptor);
                }     
                else
                {
                    return m_ParentObjectDescriptor.Definition.GetSingleElementType();
                }          
            }
        }

        public override bool CanResetValue(object component)
        {
            return !IsReadOnly;
        }

        public override object GetValue(object component)
        {
            if (m_ParentObjectDescriptor.Definition.IsArray)
            {
                return m_ParentObjectDescriptor.ChildProperties.ToArray();
            }
            return m_ParentObjectDescriptor;
        }

        public override void ResetValue(object component)
        {
            if (!IsReadOnly)
            {
                throw new NotImplementedException();
            }
        }

        public override void SetValue(object component, object value)
        {
            throw new NotImplementedException();
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        #endregion
    }
}
