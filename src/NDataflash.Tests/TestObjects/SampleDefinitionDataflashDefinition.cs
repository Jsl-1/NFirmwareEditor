using NDataflash.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDataflash.Tests.TestObjects
{
    public static class SampleDefinition
    {
        public static DataflashDefinition GetIstickPico103Definition()
        {
            var definition = new DataflashDefinition()
            {
                ProductId = "M041",
                PropertyDefinitions = new List<PropertyDefinition>()
                {
                    new ObjectPropertyDefinition()
                    {
                        Id="Params",
                        PropertyDefinitions = new List<PropertyDefinition>()
                        {
                            new UintPropertyDefinition() { Id = "PageCheckSum", ReadOnly = true },
                            new UintPropertyDefinition() { Id = "HardwareVersion", ReadOnly = true },
                            new BytePropertyDefinition() { Id = "Magic", ReadOnly = true },
                            new BytePropertyDefinition() { Id = "BootMode", ReadOnly = true, PredefinedValues = new List<PredefinedPropertyValueDefinition>()
                                {
                                    new PredefinedPropertyValueDefinition() { Id = "APROM", ValueString = "0" },
                                    new PredefinedPropertyValueDefinition() { Id = "LDROM", ValueString = "1" },
                                }
                            },
                            new BytePropertyDefinition() { Id = "SelectedMode", PredefinedValues = new List<PredefinedPropertyValueDefinition>()
                                {
                                    new PredefinedPropertyValueDefinition() { Id = "APROM", ValueString = "0" },
                                    new PredefinedPropertyValueDefinition() { Id = "LDROM", ValueString = "1" },
                                }
                            },
                            new UshortPropertyDefinition() { Id = "Power", Offset = 1},
                            new UshortPropertyDefinition() { Id = "Temperature", },
                            new UshortPropertyDefinition() { Id = "TCPower", },
                            new UshortPropertyDefinition() { Id = "VWVolts", },
                            new BytePropertyDefinition() { Id = "ThirdLineContent", PredefinedValues = new List<PredefinedPropertyValueDefinition>()
                                {
                                    new PredefinedPropertyValueDefinition() { Id = "Amps", ValueString = "0" },
                                    new PredefinedPropertyValueDefinition() { Id = "Puffs", ValueString = "1" },
                                    new PredefinedPropertyValueDefinition() { Id = "Time", ValueString = "2" },
                                    new PredefinedPropertyValueDefinition() { Id = "BatteryVoltage", ValueString = "3" },
                                    new PredefinedPropertyValueDefinition() { Id = "OutputVoltage", ValueString = "4" },
                                    new PredefinedPropertyValueDefinition() { Id = "BoardTemperature", ValueString = "5" },
                                    new PredefinedPropertyValueDefinition() { Id = "RealTimeResistance", ValueString = "6" },
                                    new PredefinedPropertyValueDefinition() { Id = "DataTime", ValueString = "7" },
                                }
                            },
                            new BytePropertyDefinition() { Id = "ResistanceType", },
                            new BytePropertyDefinition() { Id = "TemperatureAlgo", },
                            new BoolPropertyDefinition() { Id = "IsCelsius", },
                            new UshortPropertyDefinition() { Id = "Resistance", },
                            new UshortPropertyDefinition() { Id = "ResistanceTi", },
                            new UshortPropertyDefinition() { Id = "ResistanceNi", },
                            new BoolPropertyDefinition() { Id = "ResistanceTiLocked", },
                            new BoolPropertyDefinition() { Id = "ResistanceNiLocked", },
                            new BoolPropertyDefinition() { Id = "TiOn", },
                            new BoolPropertyDefinition() { Id = "StealthOn", },
                            new UshortPropertyDefinition() { Id = "TempCoefsNi", ArrayLength=21},
                            new UshortPropertyDefinition() { Id = "TempCoefsTi", ArrayLength=21},
                            new BinaryPropertyDefinition() { Id = "Status", Offset=2, ArrayLength = 2, BitDefinitions = new List<BitDefition>()
                                {
                                    new BitDefition() { Id = "Off", Index = 0 },
                                    new BitDefition() { Id = "Keylock", Index = 1 },
                                    new BitDefition() { Id = "Flipped", Index = 2 },
                                    new BitDefition() { Id = "NoLogo", Index = 3 },
                                    new BitDefition() { Id = "AnalogClock", Index = 4 },
                                    new BitDefition() { Id = "VirtualCom", Index = 5 },
                                    new BitDefition() { Id = "Storage", Index = 6 },
                                    new BitDefition() { Id = "DebugEnable", Index = 7 },

                                    new BitDefition() { Id = "X32Off", Index = 8 },
                                    new BitDefition() { Id = "TemperatureDominant", Index = 9 },
                                    new BitDefition() { Id = "Step1W", Index = 10 },
                                    new BitDefition() { Id = "DigitalClock", Index = 11 },
                                    new BitDefition() { Id = "BatteryPercent", Index = 12 },
                                    new BitDefition() { Id = "PreheatPercent", Index = 13 },
                                    new BitDefition() { Id = "WakeUpByPlusMinus", Index = 14 },
                                    new BitDefition() { Id = "UseClassicMenu", Index = 15 },
                                }
                            },
                            new UshortPropertyDefinition() { Id = "AtomizerResistance", Offset=2 },
                            new BytePropertyDefinition() { Id = "AtomizerStatus", },
                            new BytePropertyDefinition() { Id = "ShuntCorrection", },
                            new UshortPropertyDefinition() { Id = "ResistanceSS", },
                            new BoolPropertyDefinition() { Id = "ResistanceSSLocked", },
                            new BytePropertyDefinition() { Id = "UIVersion", },
                            new BytePropertyDefinition() { Id = "SelectedTCRIndex", },
                            new BytePropertyDefinition() { Id = "SelectedBatteryModel", PredefinedValues = new List<PredefinedPropertyValueDefinition>()
                                {
                                    new PredefinedPropertyValueDefinition() { Id = "Generic", ValueString = "0" },
                                    new PredefinedPropertyValueDefinition() { Id = "Samsung25R", ValueString = "1" },
                                    new PredefinedPropertyValueDefinition() { Id = "LGHG2", ValueString = "2" },
                                    new PredefinedPropertyValueDefinition() { Id = "LGHE4", ValueString = "3" },
                                    new PredefinedPropertyValueDefinition() { Id = "Samsung30Q", ValueString = "4" },
                                    new PredefinedPropertyValueDefinition() { Id = "SonyVTC4", ValueString = "5" },
                                    new PredefinedPropertyValueDefinition() { Id = "SonyVTC5", ValueString = "6" },
                                    new PredefinedPropertyValueDefinition() { Id = "Custom", ValueString = "7" },
                                }
                            },
                            new UshortPropertyDefinition() { Id = "TCR", ArrayLength=3},
                            new UshortPropertyDefinition() { Id = "ResistanceTCR", },
                            new BoolPropertyDefinition() { Id = "ResistanceTCRLocked", },
                            new BytePropertyDefinition() { Id = "ScreensaverType", PredefinedValues = new List<PredefinedPropertyValueDefinition>()
                                {
                                    new PredefinedPropertyValueDefinition() { Id = "None", ValueString = "0" },
                                    new PredefinedPropertyValueDefinition() { Id = "Clock", ValueString = "1" },
                                    new PredefinedPropertyValueDefinition() { Id = "Cube", ValueString = "2" },
                                }
                            },
                            new BytePropertyDefinition() { Id = "LastTCMode", },
                            new BytePropertyDefinition() { Id = "ScreenProtectionTime", PredefinedValues = new List<PredefinedPropertyValueDefinition>()
                                {
                                    new PredefinedPropertyValueDefinition() { Id = "Min1", ValueString = "0" },
                                    new PredefinedPropertyValueDefinition() { Id = "Min2", ValueString = "1" },
                                    new PredefinedPropertyValueDefinition() { Id = "Min5", ValueString = "2" },
                                    new PredefinedPropertyValueDefinition() { Id = "Min10", ValueString = "3" },
                                    new PredefinedPropertyValueDefinition() { Id = "Min15", ValueString = "4" },
                                    new PredefinedPropertyValueDefinition() { Id = "Min20", ValueString = "5" },
                                    new PredefinedPropertyValueDefinition() { Id = "Min30", ValueString = "6" },
                                    new PredefinedPropertyValueDefinition() { Id = "Off", ValueString = "7" },
                                }
                            },
                            new UshortPropertyDefinition() { Id = "SavedCfgRez", ArrayLength=10},
                            new UshortPropertyDefinition() { Id = "SavedCfgPwr", ArrayLength=10},
                            new UshortPropertyDefinition() { Id = "FBBest", },
                            new BytePropertyDefinition() { Id = "FBSpeed", },
                            new BytePropertyDefinition() { Id = "byte_2000033D", },
                            new BytePropertyDefinition() { Id = "Contrast", },
                            new BytePropertyDefinition() { Id = "DisabledModes", },
                            new UshortPropertyDefinition() { Id = "ClkRatio", },
                            new UshortPropertyDefinition() { Id = "PreheatPwr", },
                            new BytePropertyDefinition() { Id = "PreheatTime", },
                            new BytePropertyDefinition() { Id = "MClicks", ArrayLength=3, PredefinedValues = new List<PredefinedPropertyValueDefinition>() {
                                    new PredefinedPropertyValueDefinition() { Id = "None", ValueString = "0" },
                                    new PredefinedPropertyValueDefinition() { Id = "Edit", ValueString = "1" },
                                    new PredefinedPropertyValueDefinition() { Id = "Clock", ValueString = "2" },
                                    new PredefinedPropertyValueDefinition() { Id = "TDom", ValueString = "3" },
                                    new PredefinedPropertyValueDefinition() { Id = "NextMode", ValueString = "4" },
                                    new PredefinedPropertyValueDefinition() { Id = "OnOff", ValueString = "5" },
                                }
                            },
                            new BytePropertyDefinition() { Id = "ScreenDimTimeout", },
                            new ObjectPropertyDefinition() { Id = "CustomBattery", Offset=1, PropertyDefinitions = new List<PropertyDefinition>() {
                                    new ObjectPropertyDefinition() { Id = "PercentsVoltage", ArrayLength=11,  PropertyDefinitions = new List<PropertyDefinition>() {
                                            new UshortPropertyDefinition() { Id = "Percents", },
                                            new UshortPropertyDefinition() { Id = "Voltage", },
                                        }
                                    },
                                    new UshortPropertyDefinition() { Id = "Cutoff", },
                                    new UshortPropertyDefinition() { Id = "InternalResistance", },
                                }
                            }
                        }

                    },
                     new ObjectPropertyDefinition()
                    {
                        Id="Infos",
                        Offset = 256,
                        OffsetType = OffsetType.Absolute,
                        PropertyDefinitions = new List<PropertyDefinition>()
                        {
                            new UintPropertyDefinition() { Id = "FWVersion" },
                            new UintPropertyDefinition() { Id = "LDVersion" },
                            new UintPropertyDefinition() { Id = "fmcCID" },
                            new UintPropertyDefinition() { Id = "fmcDID" },
                            new UintPropertyDefinition() { Id = "fmcPID" },
                            new UintPropertyDefinition() { Id = "fmcUID", ArrayLength=3 },
                            new UintPropertyDefinition() { Id = "fmcUCID", ArrayLength=4 },
                            new UintPropertyDefinition() { Id = "PuffCount" },
                            new UintPropertyDefinition() { Id = "TimeCount" },
                            new StringPropertyDefinition() { Id = "ProductId" , StringLength=4},
                            new UintPropertyDefinition() { Id = "MaxHWVersion" },
                            new UshortPropertyDefinition() { Id = "MaxHWVersion" },
                            new UshortPropertyDefinition() { Id = "Year", },
                            new BytePropertyDefinition() { Id = "Month", },
                            new BytePropertyDefinition() { Id = "Day", },
                            new BytePropertyDefinition() { Id = "Hour", },
                            new BytePropertyDefinition() { Id = "Minute", },
                            new BytePropertyDefinition() { Id = "Second", },
                        }
                    }
                }
            };
            return definition;
        }
        public static DataflashDefinition GetMyEvick303_161126Definition()
        {
            var definition = new DataflashDefinition()
            {
                ProductId = "E083", // Egrip 2
                PropertyDefinitions = new List<PropertyDefinition>()
                {
                    new ObjectPropertyDefinition()
                    {
                        Id="Params",
                        PropertyDefinitions = new List<PropertyDefinition>()
                        {
                            new UintPropertyDefinition() { Id = "PageCheckSum", ReadOnly = true },
                            new UintPropertyDefinition() { Id = "HardwareVersion", ReadOnly = true },
                            new BytePropertyDefinition() { Id = "Magic", ReadOnly = true },
                            new BytePropertyDefinition() { Id = "BootMode", ReadOnly = true, PredefinedValues = new List<PredefinedPropertyValueDefinition>()
                                {
                                    new PredefinedPropertyValueDefinition() { Id = "APROM", ValueString = "0" },
                                    new PredefinedPropertyValueDefinition() { Id = "LDROM", ValueString = "1" },
                                }
                            },
                            new BytePropertyDefinition() { Id = "SelectedMode", PredefinedValues = new List<PredefinedPropertyValueDefinition>()
                                {
                                    new PredefinedPropertyValueDefinition() { Id = "APROM", ValueString = "0" },
                                    new PredefinedPropertyValueDefinition() { Id = "LDROM", ValueString = "1" },
                                }
                            },
                            new UshortPropertyDefinition() { Id = "Power", Offset = 1},
                            new UshortPropertyDefinition() { Id = "Temperature", },
                            new UshortPropertyDefinition() { Id = "TCPower", },
                            new UshortPropertyDefinition() { Id = "VWVolts", },
                            new BytePropertyDefinition() { Id = "ThirdLineContent", PredefinedValues = new List<PredefinedPropertyValueDefinition>()
                                {
                                    new PredefinedPropertyValueDefinition() { Id = "Amps", ValueString = "0" },
                                    new PredefinedPropertyValueDefinition() { Id = "Puffs", ValueString = "1" },
                                    new PredefinedPropertyValueDefinition() { Id = "Time", ValueString = "2" },
                                    new PredefinedPropertyValueDefinition() { Id = "BatteryVoltage", ValueString = "3" },
                                    new PredefinedPropertyValueDefinition() { Id = "OutputVoltage", ValueString = "4" },
                                    new PredefinedPropertyValueDefinition() { Id = "BoardTemperature", ValueString = "5" },
                                    new PredefinedPropertyValueDefinition() { Id = "RealTimeResistance", ValueString = "6" },
                                    new PredefinedPropertyValueDefinition() { Id = "DataTime", ValueString = "7" },
                                }
                            },
                            new BytePropertyDefinition() { Id = "ResistanceType", },
                            new BytePropertyDefinition() { Id = "TemperatureAlgo", },
                            new BoolPropertyDefinition() { Id = "IsCelsius", },
                            new UshortPropertyDefinition() { Id = "Resistance", },
                            new UshortPropertyDefinition() { Id = "ResistanceTi", },
                            new UshortPropertyDefinition() { Id = "ResistanceNi", },
                            new BoolPropertyDefinition() { Id = "ResistanceTiLocked", },
                            new BoolPropertyDefinition() { Id = "ResistanceNiLocked", },
                            new BoolPropertyDefinition() { Id = "TiOn", },
                            new BoolPropertyDefinition() { Id = "StealthOn", },
                            new UshortPropertyDefinition() { Id = "TempCoefsNi", ArrayLength=21},
                            new UshortPropertyDefinition() { Id = "TempCoefsTi", ArrayLength=21},
                            new BinaryPropertyDefinition() { Id = "Status", Offset=2, ArrayLength = 2, BitDefinitions = new List<BitDefition>()
                                {
                                    new BitDefition() { Id = "Off", Index = 0 },
                                    new BitDefition() { Id = "Keylock", Index = 1 },
                                    new BitDefition() { Id = "Flipped", Index = 2 },
                                    new BitDefition() { Id = "NoLogo", Index = 3 },
                                    new BitDefition() { Id = "AnalogClock", Index = 4 },
                                    new BitDefition() { Id = "VirtualCom", Index = 5 },
                                    new BitDefition() { Id = "Storage", Index = 6 },
                                    new BitDefition() { Id = "DebugEnable", Index = 7 },

                                    new BitDefition() { Id = "X32Off", Index = 8 },
                                    new BitDefition() { Id = "TemperatureDominant", Index = 9 },
                                    new BitDefition() { Id = "Step1W", Index = 10 },
                                    new BitDefition() { Id = "DigitalClock", Index = 11 },
                                    new BitDefition() { Id = "BatteryPercent", Index = 12 },
                                    new BitDefition() { Id = "PreheatPercent", Index = 13 },
                                    new BitDefition() { Id = "WakeUpByPlusMinus", Index = 14 },
                                    new BitDefition() { Id = "UseClassicMenu", Index = 15 },
                                }
                            },
                            new UshortPropertyDefinition() { Id = "AtomizerResistance", Offset=2 },
                            new BytePropertyDefinition() { Id = "AtomizerStatus", },
                            new BytePropertyDefinition() { Id = "ShuntCorrection", },
                            new UshortPropertyDefinition() { Id = "ResistanceSS", },
                            new BoolPropertyDefinition() { Id = "ResistanceSSLocked", },
                            new BytePropertyDefinition() { Id = "UIVersion", },
                            new BytePropertyDefinition() { Id = "SelectedTCRIndex", },
                            new BytePropertyDefinition() { Id = "SelectedBatteryModel", PredefinedValues = new List<PredefinedPropertyValueDefinition>()
                                {
                                    new PredefinedPropertyValueDefinition() { Id = "Generic", ValueString = "0" },
                                    new PredefinedPropertyValueDefinition() { Id = "Samsung25R", ValueString = "1" },
                                    new PredefinedPropertyValueDefinition() { Id = "LGHG2", ValueString = "2" },
                                    new PredefinedPropertyValueDefinition() { Id = "LGHE4", ValueString = "3" },
                                    new PredefinedPropertyValueDefinition() { Id = "Samsung30Q", ValueString = "4" },
                                    new PredefinedPropertyValueDefinition() { Id = "SonyVTC4", ValueString = "5" },
                                    new PredefinedPropertyValueDefinition() { Id = "SonyVTC5", ValueString = "6" },
                                    new PredefinedPropertyValueDefinition() { Id = "Custom", ValueString = "7" },
                                }
                            },
                            new UshortPropertyDefinition() { Id = "TCR", ArrayLength=3},
                            new UshortPropertyDefinition() { Id = "ResistanceTCR", },
                            new BoolPropertyDefinition() { Id = "ResistanceTCRLocked", },
                            new BytePropertyDefinition() { Id = "ScreensaverType", PredefinedValues = new List<PredefinedPropertyValueDefinition>()
                                {
                                    new PredefinedPropertyValueDefinition() { Id = "None", ValueString = "0" },
                                    new PredefinedPropertyValueDefinition() { Id = "Clock", ValueString = "1" },
                                    new PredefinedPropertyValueDefinition() { Id = "Cube", ValueString = "2" },                                   
                                }
                            },
                            new BytePropertyDefinition() { Id = "LastTCMode", },
                            new BytePropertyDefinition() { Id = "ScreenProtectionTime", PredefinedValues = new List<PredefinedPropertyValueDefinition>()
                                {
                                    new PredefinedPropertyValueDefinition() { Id = "Min1", ValueString = "0" },
                                    new PredefinedPropertyValueDefinition() { Id = "Min2", ValueString = "1" },
                                    new PredefinedPropertyValueDefinition() { Id = "Min5", ValueString = "2" },
                                    new PredefinedPropertyValueDefinition() { Id = "Min10", ValueString = "3" },
                                    new PredefinedPropertyValueDefinition() { Id = "Min15", ValueString = "4" },
                                    new PredefinedPropertyValueDefinition() { Id = "Min20", ValueString = "5" },
                                    new PredefinedPropertyValueDefinition() { Id = "Min30", ValueString = "6" },
                                    new PredefinedPropertyValueDefinition() { Id = "Off", ValueString = "7" },
                                }
                            },
                            new UshortPropertyDefinition() { Id = "SavedCfgRez", ArrayLength=10},
                            new UshortPropertyDefinition() { Id = "SavedCfgPwr", ArrayLength=10},
                            new UshortPropertyDefinition() { Id = "FBBest", },
                            new BytePropertyDefinition() { Id = "FBSpeed", },
                            new BytePropertyDefinition() { Id = "byte_2000033D", },
                            new BytePropertyDefinition() { Id = "Contrast", },
                            new BytePropertyDefinition() { Id = "DisabledModes", },
                            new UshortPropertyDefinition() { Id = "ClkRatio", },
                            new UshortPropertyDefinition() { Id = "PreheatPwr", },
                            new BytePropertyDefinition() { Id = "PreheatTime", },
                            new BytePropertyDefinition() { Id = "MClicks", ArrayLength=3, PredefinedValues = new List<PredefinedPropertyValueDefinition>() {
                                    new PredefinedPropertyValueDefinition() { Id = "None", ValueString = "0" },
                                    new PredefinedPropertyValueDefinition() { Id = "Edit", ValueString = "1" },
                                    new PredefinedPropertyValueDefinition() { Id = "Clock", ValueString = "2" },
                                    new PredefinedPropertyValueDefinition() { Id = "TDom", ValueString = "3" },
                                    new PredefinedPropertyValueDefinition() { Id = "NextMode", ValueString = "4" },
                                    new PredefinedPropertyValueDefinition() { Id = "OnOff", ValueString = "5" },
                                }
                            },
                            new BytePropertyDefinition() { Id = "ScreenDimTimeout", },
                            new ObjectPropertyDefinition() { Id = "CustomBattery", Offset=1, PropertyDefinitions = new List<PropertyDefinition>() {
                                    new ObjectPropertyDefinition() { Id = "PercentsVoltage", ArrayLength=11,  PropertyDefinitions = new List<PropertyDefinition>() {
                                            new UshortPropertyDefinition() { Id = "Percents", },
                                            new UshortPropertyDefinition() { Id = "Voltage", },
                                        }
                                    },
                                    new UshortPropertyDefinition() { Id = "Cutoff", },
                                    new UshortPropertyDefinition() { Id = "InternalResistance", },
                                }
                            }
                        }

                    },
                     new ObjectPropertyDefinition()
                    {
                        Id="Infos",
                        Offset = 256,
                        OffsetType = OffsetType.Absolute,
                        PropertyDefinitions = new List<PropertyDefinition>()
                        {
                            new UintPropertyDefinition() { Id = "FWVersion" },
                            new UintPropertyDefinition() { Id = "LDVersion" },
                            new UintPropertyDefinition() { Id = "fmcCID" },
                            new UintPropertyDefinition() { Id = "fmcDID" },
                            new UintPropertyDefinition() { Id = "fmcPID" },
                            new UintPropertyDefinition() { Id = "fmcUID", ArrayLength=3 },
                            new UintPropertyDefinition() { Id = "fmcUCID", ArrayLength=4 },
                            new UintPropertyDefinition() { Id = "PuffCount" },
                            new UintPropertyDefinition() { Id = "TimeCount" },
                            new StringPropertyDefinition() { Id = "ProductId" , StringLength=4},
                            new UintPropertyDefinition() { Id = "MaxHWVersion" },
                            new UshortPropertyDefinition() { Id = "MaxHWVersion" },
                            new UshortPropertyDefinition() { Id = "Year", },
                            new BytePropertyDefinition() { Id = "Month", },
                            new BytePropertyDefinition() { Id = "Day", },
                            new BytePropertyDefinition() { Id = "Hour", },
                            new BytePropertyDefinition() { Id = "Minute", },
                            new BytePropertyDefinition() { Id = "Second", },
                        }
                    }
                }
            };
            return definition;
        }

        public static DataflashDefinition GetDataflashDefinition()
        {
            var definition = new DataflashDefinition()
            {
                PropertyDefinitions = new List<PropertyDefinition>()
                {
                    new ObjectPropertyDefinition()
                    {
                        Id="CustomObject1",
                        PropertyDefinitions = new List<PropertyDefinition>()
                        {

                        }

                    },
                     new ObjectPropertyDefinition()
                    {
                        Id="CustomObject2",
                        PropertyDefinitions = new List<PropertyDefinition>()
                        { 

                        }
                    }
                }
            };
            return definition;
        }
    }
}
