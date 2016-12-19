using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDataflash.Definition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NDataflash.Tests
{
    [TestClass]
    public class DataflashDefinitionTests
    {
        [TestMethod]
        public void SerializeDefinition()
        {
            var definition = TestObjects.SampleDefinition.GetIstickPico103Definition();
            
            using (var ms = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(DataflashDefinition));
                serializer.Serialize(ms, definition);
               
                ms.Seek(0, SeekOrigin.Begin);
                var xml = Encoding.UTF8.GetString(ms.ToArray());
            }

            definition = TestObjects.SampleDefinition.GetMyEvick303_161126Definition();
            using (var ms = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(DataflashDefinition));
                serializer.Serialize(ms, definition);

                ms.Seek(0, SeekOrigin.Begin);
                var xml = Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        [TestMethod]
        public void UnserializeDefinition()
        {
            var serializer = new XmlSerializer(typeof(DataflashDefinition));
            using(var reader = new StringReader(Properties.Resources.SampleDefinition))
            {
                var definition = serializer.Deserialize(reader);
            }
        }

        //[TestMethod]
        //public void CheckPropertyTypeByteLength()
        //{
        //    using (var ms = new MemoryStream())
        //    using (var bw = new BinaryWriter(ms))
        //    {
        //        bw.Write(uint.MaxValue);
        //        //4
        //    }

        //    using (var ms = new MemoryStream())
        //    using (var bw = new BinaryWriter(ms))
        //    {
        //        bw.Write(ushort.MaxValue);
        //        //2
        //    }

        //    using (var ms = new MemoryStream())
        //    using (var bw = new BinaryWriter(ms))
        //    {
        //        bw.Write(true);
        //        //1
        //    }

        //}
    }
}
