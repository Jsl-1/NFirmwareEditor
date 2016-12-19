using NDataflash.Definition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NDataflash.Descriptor
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DataflashObjectDescriptor : ICustomTypeDescriptor
    {
        public Int32 AbsoluteOffset { get; internal set; }

        public Int32 ArrayIndex { get; internal set; } = -1;

        public PropertyDefinition Definition { get; internal set; }

        public List<DataflashObjectDescriptor> ChildProperties { get; internal set; } = new List<DataflashObjectDescriptor>();

        #region ICustomTypeDescriptor members

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return Definition.Id + " object"; //TypeDescriptor.GetClassName(this, true);
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
            return GetProperties(new Attribute[] { new TypeConverterAttribute(typeof(ExpandableObjectConverter))});
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            List<PropertyDescriptor> pds = new List<PropertyDescriptor>();
            if (Definition.IsArray)
            {

            }
            else if( Definition is ObjectPropertyDefinition)
            {
                foreach (var property in ChildProperties)
                {
                    if (property.Definition.IsArray)
                    {
                        var attr = attributes.ToList();
                        attr.Add(new TypeConverterAttribute(typeof(ExpandableObjectConverter)));
                        attr.Add(new CategoryAttribute("DataFlash"));
                        var propertyDescriptor = new CustomValuePropertyDescriptor(property.Definition.Id, property.ChildProperties.ToArray(), attr.ToArray());
                    }
                    else
                    {
                        var propertyDescriptor = new DataflashPropertyDescriptor(property, property.Definition.Id);
                        pds.Add(propertyDescriptor);
                    }
                }
            }
            else
            {

            }
            return new PropertyDescriptorCollection(pds.ToArray());
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return null;
        }

        #endregion
    }

  
}
