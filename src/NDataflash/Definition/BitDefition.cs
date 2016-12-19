using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NDataflash.Definition
{
    public class BitDefition
    {
        [XmlAttribute]
        public String Id { get; set; }

        [XmlAttribute]
        public Int32 Index { get; set; }
    }
}
