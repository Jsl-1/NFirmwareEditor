using NDataflash;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace NDataflashEditor.Managers
{
    internal class DataflashObjectDescriptor : ICustomTypeDescriptor
    {
        private DataflashManager m_manager;

        public DataflashObjectDescriptor(DataflashManager manager)
        {
            m_manager = manager;
        }


        #region ICustomTypeDescriptor Members

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(m_manager.Dataflash, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(m_manager.Dataflash, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(m_manager.Dataflash, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(m_manager.Dataflash, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(m_manager.Dataflash, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(m_manager.Dataflash, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(m_manager.Dataflash, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(m_manager.Dataflash, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(m_manager.Dataflash, attributes, true);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return this.GetProperties(new Attribute[0]);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            List<PropertyDescriptor> pds = new List<PropertyDescriptor>();
            foreach (var property in m_manager.ProperyDefinitions)
            {
                var propertyDescriptor = new DataflashPropertyDescriptor(m_manager, property, attributes);

                pds.Add(propertyDescriptor);
            }
            return new PropertyDescriptorCollection(pds.ToArray());
        }

        public object GetPropertyOwner(PropertyDescriptor descriptor)
        {
            return m_manager.Dataflash;
        }

        #endregion
    }
}
