using NDataflash.Definition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NDataflash
{
    public class DataflashManager
    {
        private Dataflash m_dataflash;
        private DataflashDefinition m_definition;
        private byte[] m_initialData;
        private Dictionary<PropertyDefinition, Int32> m_PropertyOffsets;

        public DataflashManager(Dataflash dataflash, DataflashDefinition definition)
        {
            m_dataflash = dataflash;
            m_definition = definition;
            m_initialData = new byte[Dataflash.Data.Length];
            Buffer.BlockCopy(Dataflash.Data, 0, m_initialData, 0, Dataflash.Data.Length);

            _ReadDataflash();
        }

        public byte[] InitialData
        {
            get { return m_initialData; }
        }
        public Dataflash Dataflash
        {
            get { return m_dataflash; }
        }

        public DataflashDefinition Definition
        {
            get { return m_definition; }
        }

        public Dictionary<PropertyDefinition, Int32> PropertyOffsets
        {
            get { return m_PropertyOffsets; }
        }

        public IEnumerable<PropertyDefinition> ProperyDefinitions
        {
            get
            {
                return m_PropertyOffsets.Keys;
            }
        }


        private void _ReadDataflash()
        {
            m_PropertyOffsets = new Dictionary<PropertyDefinition, Int32>();

            using (var br = new BinaryReader(new MemoryStream(Dataflash.Data)))
            {
                foreach (var property in Definition.PropertyDefinitions)
                {                    
                     ReadRecursive(property, br, PropertyOffsets);
                }               
            }
        }

        private object ReadRecursive(PropertyDefinition property, BinaryReader br, Dictionary<PropertyDefinition, Int32> outputPropertyOffsetCache = null)
        {
            if (outputPropertyOffsetCache == null)
                outputPropertyOffsetCache = new Dictionary<PropertyDefinition, Int32>();

            object result = null;
            if (property.OffsetType == OffsetType.Absolute)
            {
                br.BaseStream.Seek(property.Offset, SeekOrigin.Begin);
            }
            else if (property.OffsetType == OffsetType.Relative)
            {
                br.BaseStream.Seek(property.Offset, SeekOrigin.Current);
            }

            Int32 propertyOffset= -1;
            if(!outputPropertyOffsetCache.TryGetValue(property, out propertyOffset))
            {
                propertyOffset = Convert.ToInt32(br.BaseStream.Position);
                outputPropertyOffsetCache.Add(property, propertyOffset);
            }

            var length = property.IsArray ? property.ArrayLength : 1;
            var values = Array.CreateInstance(property.GetSingleElementType(), length);
            
            for (var i = 0; i < length; i++)
            {
                if (property is IPropertyDefinitionContainer)
                {
                    var childProperties = ((IPropertyDefinitionContainer)property).PropertyDefinitions;
                    var childvalues = new object[childProperties.Count];
                    for(var j = 0; j< childProperties.Count;j++ )
                    {
                        childvalues[j] = ReadRecursive(childProperties[j], br, outputPropertyOffsetCache);
                    }
                    values.SetValue(childvalues, i);
                }
                else
                {
                    values.SetValue(_ReadSinglePropertyValue(property, br), i);
                }
            }
            result = property.IsArray ? values : values.GetValue(0);

            return result;
        }

        private object _ReadSinglePropertyValue(PropertyDefinition property, BinaryReader br)
        {
            if (property is BytePropertyDefinition) return br.ReadByte();
            else if (property is UshortPropertyDefinition) return br.ReadUInt16();
            else if (property is BoolPropertyDefinition) return br.ReadBoolean();
            else if (property is UintPropertyDefinition) return br.ReadUInt32();
            else if (property is StringPropertyDefinition)
            {
                List<byte> bytes = new List<byte>();
                for (var i = 0; i < ((StringPropertyDefinition)property).StringLength; i++)
                {
                    bytes.Add(br.ReadByte());
                }
                return Encoding.ASCII.GetString(bytes.ToArray());
            }
            throw new InvalidOperationException("Invalid property type");
        }

        private object _WriteSinglePropertyValue(PropertyDefinition property,object value, BinaryWriter bw)
        {
            if (property is BytePropertyDefinition) bw.Write((byte)value);
            else if (property is UshortPropertyDefinition) bw.Write((ushort)value);
            else if (property is BoolPropertyDefinition) bw.Write((bool)value);
            else if (property is UintPropertyDefinition) bw.Write((uint)value);
            else if (property is StringPropertyDefinition)
            {
                var valueBytes = Encoding.ASCII.GetBytes((string)value);
                var length = ((StringPropertyDefinition)property).StringLength;
                var result = new byte[length];
                Buffer.BlockCopy(valueBytes, 0, result, 0, valueBytes.Length);
                bw.Write(result);
            }
            throw new InvalidOperationException("Invalid property type");
        }

        public byte[] GetPropertyBytes(PropertyDefinition property)
        {
            return _GetPropertyBytes(property, Dataflash.Data);
        }

        private byte[] _GetPropertyBytes(PropertyDefinition property, byte[] dataflashBytes)
        {
            var propertyByteLength = property.GetPropertyByteLength();
            var offset = m_PropertyOffsets[property];
            var result = new byte[propertyByteLength];
            Buffer.BlockCopy(dataflashBytes, offset, result, 0, propertyByteLength);
            return result;
        }

        public object GetPropertyValue(string dataMember)
        {
            var property = this.Definition[dataMember];
            return GetPropertyValue(property);
        }

        public object GetPropertyValue(PropertyDefinition property)
        {
            return _GetPropertyValue(property, Dataflash.Data);
        }

        public object GetInitialPropertyValue(PropertyDefinition property)
        {
            return _GetPropertyValue(property, m_initialData);
        }

        private object _GetPropertyValue(PropertyDefinition property, byte[] dataflashBytes)
        {
            var offset = PropertyOffsets[property];
            using (var reader = new BinaryReader(new MemoryStream(m_dataflash.Data)))
            {
                reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                if (property.OffsetType == OffsetType.Relative)
                    reader.BaseStream.Seek(-property.Offset, SeekOrigin.Current);
                return ReadRecursive(property, reader);
            }
        }

     
        public void SetPropertyValue(PropertyDefinition property, object value)
        {
            var bytes = property.ConvertValueToByteArray(value);
            var length = property.GetPropertyByteLength();
            Buffer.BlockCopy(bytes, 0, Dataflash.Data, m_PropertyOffsets[property], length);
        }
    }
}
