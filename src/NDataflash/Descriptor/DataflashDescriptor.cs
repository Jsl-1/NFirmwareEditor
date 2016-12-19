using NDataflash.Definition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NDataflash.Descriptor
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DataflashDescriptor : ICustomTypeDescriptor
    {
        private List<DataflashObjectDescriptor> m_PropertyValues;


        public List<DataflashObjectDescriptor> PropertyValues
        {
            get { return m_PropertyValues; }
        }     

        public DataflashManager Manager { get; set; }

        public void ReadDefinition(DataflashDefinition dataflashDefinition)
        {
            if (m_PropertyValues != null)
                m_PropertyValues.Clear();
            m_PropertyValues = new List<DataflashObjectDescriptor>();

            var currentBinaryPosition = 0;
            foreach (var propertyDefinition in dataflashDefinition.PropertyDefinitions)
            {
                m_PropertyValues.Add(_ReadRecursive(propertyDefinition, ref currentBinaryPosition));
            }
        }

        private DataflashObjectDescriptor _ReadRecursive(PropertyDefinition propertyDefinition, ref Int32 currentBinaryPosition, Int32? arrayIndex = null)
        {
            if (propertyDefinition.OffsetType == OffsetType.Absolute)
                currentBinaryPosition = propertyDefinition.Offset;
            else if (propertyDefinition.OffsetType == OffsetType.Relative)
                currentBinaryPosition += propertyDefinition.Offset;

            DataflashObjectDescriptor descriptor = new DataflashObjectDescriptor();
            descriptor.AbsoluteOffset = currentBinaryPosition;
            descriptor.Definition = propertyDefinition;
            descriptor.ArrayIndex = arrayIndex ?? -1;

            if (propertyDefinition is IPropertyDefinitionContainer)
            {

                if (propertyDefinition.IsArray && arrayIndex == null)
                {
                    for (var i = 0; i < propertyDefinition.ArrayLength; i++)
                    {
                        descriptor.ChildProperties.Add(_ReadRecursive(propertyDefinition, ref currentBinaryPosition, i));
                    }
                }
                else
                {
                    foreach (var pChild in ((IPropertyDefinitionContainer)propertyDefinition).PropertyDefinitions)
                    {
                        descriptor.ChildProperties.Add(_ReadRecursive(pChild, ref currentBinaryPosition, arrayIndex));
                    }
                }
            }
            else
            {
                if (propertyDefinition.IsArray && arrayIndex == null)
                {
                    for (var i = 0; i < propertyDefinition.ArrayLength; i++)
                    {
                        descriptor.ChildProperties.Add(_ReadRecursive(propertyDefinition, ref currentBinaryPosition, i));
                    }
                }
                else
                {
                    currentBinaryPosition += propertyDefinition.GetSingleElementByteLength();
                }
            }
            return descriptor;
        }


        #region ICustomTypeDescriptor Members 

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return GetEvents(new Attribute[0]);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return GetProperties(new Attribute[0]);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            List<PropertyDescriptor> pds = new List<PropertyDescriptor>();

            foreach (var property in PropertyValues)
            {
                var attr = attributes.ToList();
                attr.Add(new TypeConverterAttribute(typeof(ExpandableObjectConverter)));
                attr.Add(new CategoryAttribute("DataFlash"));
                var propertyDescriptor = new CustomValuePropertyDescriptor(property.Definition.Id, property, attr.ToArray());
                pds.Add(propertyDescriptor);
            }

            return new PropertyDescriptorCollection(pds.ToArray());
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion
    }
}
