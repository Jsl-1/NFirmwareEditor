using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDataflash.Tests
{
    [TestClass]
    public class DataflashManagerTests
    {
        [TestMethod]
        public void ReadIStickPicoDataflashDefinition()
        {
            var definition = TestObjects.SampleDefinition.GetIstickPico103Definition();
            var manager = new DataflashManager(new Dataflash() { Data = Properties.Resources.IStickPico103Dataflash }, definition);
        }

        [TestMethod]
        public void CheckIstickPico103DataflashProperties()
        {
            var definition = TestObjects.SampleDefinition.GetIstickPico103Definition();
            var manager = new DataflashManager(new Dataflash() { Data = Properties.Resources.IStickPico103Dataflash }, definition);

            var hardwareVersion = (uint)manager.GetPropertyValue("Params.HardwareVersion");
            if (hardwareVersion != 100)
                Assert.Fail("Hardware Version error");
        }

        [TestMethod]
        public void DataflashIsEqualAfterWritingValues()
        {
            var definition = TestObjects.SampleDefinition.GetIstickPico103Definition();
            var manager = new DataflashManager(new Dataflash() { Data = Properties.Resources.IStickPico103Dataflash }, definition);
            foreach(var property in manager.ProperyDefinitions)
            {
                var value = manager.GetPropertyValue(property);
                manager.SetPropertyValue(property, value);


                for (var i = 0; i < manager.InitialData.Length; i++)
                {
                    if (manager.InitialData[i] != manager.Dataflash.Data[i])
                        Assert.Fail("Data differ after setting same value at byte " + i.ToString());
                }
            }
        }

        [TestMethod]
        public void AllPropertyWritesEqualsRead()
        {
            var definition = TestObjects.SampleDefinition.GetIstickPico103Definition();
            var manager = new DataflashManager(new Dataflash() { Data = Properties.Resources.IStickPico103Dataflash }, definition);
            foreach (var property in manager.ProperyDefinitions)
            {
                if(!(property is NDataflash.Definition.ObjectPropertyDefinition))
                {
                    var propertyType = property.GetPropertyType();
                    if (propertyType.IsArray)
                    {

                    }
                    else
                    {
                        object newVal = null;
                        if(propertyType == typeof(uint))
                        {
                            newVal = uint.MaxValue;
                        }
                        else if (propertyType == typeof(ushort))
                        {
                            newVal = ushort.MaxValue; 
                        }
                        else if (propertyType == typeof(Boolean))
                        {
                            newVal = true;
                        }
                        if (propertyType == typeof(byte))
                        {
                            newVal = byte.MaxValue;
                        }
                        if (propertyType == typeof(string))
                        {
                            var charArray = new char[((NDataflash.Definition.StringPropertyDefinition)property).StringLength];
                            for(var i=0; i< charArray.Length; i++)
                            {
                                charArray[i] = 'X';
                            }
                            newVal = new String(charArray);
                        }

                        manager.SetPropertyValue(property, newVal);
                        var v = manager.GetPropertyValue(property);
                        if(!v.Equals(newVal))
                            Assert.Fail("Read and Write value are not equal");
                    }
                }               
            }
        }

    }
}
