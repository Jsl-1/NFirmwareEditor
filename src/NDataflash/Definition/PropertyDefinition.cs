using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NDataflash.Definition
{
    public abstract class PropertyDefinition
    {
        [XmlAttribute]
        public string Id { get; set; }

        [XmlAttribute]
        public String DisplayName { get; set; }

        [DefaultValue(0)]
        [XmlAttribute]
        public Int32 ArrayLength { get; set; }

        [DefaultValue(0)]
        [XmlAttribute]
        public Int32 Offset { get; set; }

        [DefaultValue(OffsetType.Relative)]
        [XmlAttribute]
        public OffsetType OffsetType { get; set; }

        [DefaultValue(false)]
        [XmlAttribute]
        public virtual bool ReadOnly { get; set; }

        [XmlElement("Value")]
        public List<PredefinedPropertyValueDefinition> PredefinedValues { get; set; } = new List<PredefinedPropertyValueDefinition>();

        [XmlIgnore]
        public IPropertyDefinitionContainer ParentContainer { get; set; }

        [XmlIgnore]
        public IPropertyDefinitionContainer ParentDefinition { get; set; }

        [XmlIgnore]
        public bool IsArray
        {
            get
            {
                return ArrayLength > 0;
            }
        }

        public abstract Type GetSingleElementType();

        protected abstract Int32 GetSingleElementByteLength();

        public Type GetPropertyType()
        {
            var type = GetSingleElementType();
            if (IsArray)
                return type.MakeArrayType();
            else
                return type;
        }

        public Int32 GetPropertyByteLength()
        {
            var singleElementLEngth = this.GetSingleElementByteLength();
            if (IsArray)
                return ArrayLength * singleElementLEngth;
            else
                return singleElementLEngth;
        }

        public byte[] ConvertValueToByteArray(object value)
        {
            if(IsArray)
            {
                using (var ms = new MemoryStream())
                using (var bw = new BinaryWriter(ms))
                {
                    for (var i = 0; i < ArrayLength; i++)
                    {
                        var v = ((Array)value).GetValue(i);
                        var l = this.GetSingleElementByteLength();
                        ms.Write(ConvertSingleValueToByteArray(v), 0, l);
                    }
                    var result = ms.ToArray();
                    return result;
                }
            }
            else
            {
                return ConvertSingleValueToByteArray(value);
            }
        }

        protected abstract byte[] ConvertSingleValueToByteArray(object value);
    }

    public class UintPropertyDefinition : PropertyDefinition
    {
        public override Type GetSingleElementType()
        {
            return typeof(uint);
        }

        protected override int GetSingleElementByteLength()
        {
            return 4;
        }

        protected override byte[] ConvertSingleValueToByteArray(object value)
        {
            using (var ms = new MemoryStream())
            using(var bw = new BinaryWriter(ms))
            {
                bw.Write((uint)value);
                return ms.ToArray();
            }
        }

    }

    public class BytePropertyDefinition : PropertyDefinition
    {
        public override Type GetSingleElementType()
        {
            return typeof(byte);
        }

        protected override int GetSingleElementByteLength()
        {
            return 1;
        }

        protected override byte[] ConvertSingleValueToByteArray(object value)
        {
            return new byte[1] { (byte)value };
        }
    }

    public class UshortPropertyDefinition : PropertyDefinition
    {
        public override Type GetSingleElementType()
        {
            return typeof(ushort);
        }

        protected override int GetSingleElementByteLength()
        {
            return 2;
        }

        protected override byte[] ConvertSingleValueToByteArray(object value)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((ushort)value);
                return ms.ToArray();
            }
        }
    }

    public class StringPropertyDefinition : PropertyDefinition
    {
        public override Type GetSingleElementType()
        {
            return typeof(string);
        }

        [DefaultValue(0)]
        [XmlAttribute]
        public Int32 StringLength { get; set; }

        protected override int GetSingleElementByteLength()
        {
            return StringLength;
        }

        protected override byte[] ConvertSingleValueToByteArray(object value)
        {
            using (var ms = new MemoryStream(StringLength))
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(ASCIIEncoding.ASCII.GetBytes(value.ToString()));
                return ms.ToArray();
            }
        }
    }

    public class BoolPropertyDefinition : PropertyDefinition
    {
        public override Type GetSingleElementType()
        {
            return typeof(bool);
        }

        protected override int GetSingleElementByteLength()
        {
            return 1;
        }

        protected override byte[] ConvertSingleValueToByteArray(object value)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((bool)value);
                return ms.ToArray();
            }
        }
    }

    public class BinaryPropertyDefinition : BytePropertyDefinition
    {
        //[XmlElement("bit", typeof(BitDefition))]
        [XmlIgnore]
        public List<BitDefition> BitDefinitions { get; set; } = new List<BitDefition>();

    }


    public enum OffsetType
    {
        Relative = 0,
        Absolute = 1,       
    }
}
