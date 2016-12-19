using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NDataflash.Definition
{
    public class DataflashDefinition : IPropertyDefinitionContainer
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

        [XmlAttribute]
        public string ProductId { get; set; }

        //[XmlAttribute]
        //public LayoutDefinition LayoutDefinition { get; set; } = new LayoutDefinition();

        [XmlIgnore]
        public String FileName { get; set; }

        [XmlIgnore]
        public String Sha { get; set; }


        [XmlIgnore]
        public PropertyDefinition this[string dataMember]
        {
            get
            {
                return IPropertyDefinitionContainerHelper.GetChildPropertyDefinition(this, dataMember);
            }            
        }

        [XmlIgnore]
        public string Name
        {
            get
            {
                return ProductId;
            }
        }



        public PropertyDefinition GetChildPropertyDefinition(string dataMember)
        {
            return this.GetChildPropertyDefinition(dataMember);
        }
    }
}
