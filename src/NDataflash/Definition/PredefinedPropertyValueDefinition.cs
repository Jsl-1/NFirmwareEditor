using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NDataflash.Definition
{
    public class PredefinedPropertyValueDefinition
    {
        [XmlAttribute]
        public string Id { get; set; }

        [XmlAttribute("Value")]
        public string ValueString { get; set; }
    }
}
