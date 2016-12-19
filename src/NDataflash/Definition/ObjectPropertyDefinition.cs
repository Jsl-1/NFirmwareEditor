using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NDataflash.Definition
{
    public class ObjectPropertyDefinition : PropertyDefinition, IPropertyDefinitionContainer
    {
        [XmlArray("Properties")]
        [XmlArrayItem(typeof(UintPropertyDefinition), ElementName = "uint")]
        [XmlArrayItem(typeof(UshortPropertyDefinition), ElementName = "ushort")]
        [XmlArrayItem(typeof(BytePropertyDefinition), ElementName = "byte")]
        [XmlArrayItem(typeof(BoolPropertyDefinition), ElementName = "bool")]
        [XmlArrayItem(typeof(StringPropertyDefinition), ElementName = "string")]
        [XmlArrayItem(typeof(BinaryPropertyDefinition), ElementName = "binary")]
        [XmlArrayItem(typeof(ObjectPropertyDefinition), ElementName = "object")]
        public List<PropertyDefinition> PropertyDefinitions { get; set; } = new List<PropertyDefinition>();

        public PropertyDefinition GetChildPropertyDefinition(string dataMember)
        {
            return IPropertyDefinitionContainerHelper.GetChildPropertyDefinition(this, dataMember);
        }

        [XmlIgnore]
        public string Name
        {
            get
            {
                return Id;
            }
        }

        public override Type GetSingleElementType()
        {
            return typeof(object);
        }

        protected override int GetSingleElementByteLength()
        {
            int length = 0;
            foreach(var property in this.PropertyDefinitions)
            {
                if (property.OffsetType == OffsetType.Relative)
                {
                    length += property.Offset;
                }                
                length += property.GetPropertyByteLength();
            }
            return length;
        }

        protected override byte[] ConvertSingleValueToByteArray(object value)
        {
            using (var ms = new MemoryStream())
            using(var bw = new BinaryWriter(ms))
            {
                var values = (ArrayList)value;
                for (var i = 0; i < PropertyDefinitions.Count; i++)
                {
                    var property = PropertyDefinitions[i];
                    if (property.OffsetType == OffsetType.Relative)
                    {
                        ms.Seek(property.Offset, SeekOrigin.Current);
                    }
                    ms.Write(property.ConvertValueToByteArray(values[i]), 0, property.GetPropertyByteLength());
                    
                }
                return ms.ToArray();
            }           
        }
    }
}
