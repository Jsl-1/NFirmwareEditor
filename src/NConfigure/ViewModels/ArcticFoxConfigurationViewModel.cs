
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NToolbox.Models;
using NCore;
using System.Globalization;
using NCore.USB;

namespace NToolbox.ViewModels
{



    public  class ArcticFoxConfigurationViewModel
    {
        private const ushort MaxPower = 2500;
        private const byte MaxBatteries = 3;
        private const int MinimumSupportedBuildNumber = 161223;
        private const int MaximumSupportedSettingsVersion = 5;


        private ArcticFoxConfiguration Configuration { get; set; }

        public ArcticFoxConfigurationViewModel()
        {
            //Configuration = new ArcticFoxConfiguration();
            Configuration = BinaryStructure.Read<ArcticFoxConfiguration>(Properties.Resources.new_configuration);
        }

        public void ReadConfigurationFromDevice()
        {
            var data = HidConnector.Instance.ReadConfiguration();
            Configuration = BinaryStructure.Read<ArcticFoxConfiguration>(data);
        }

        public void WriteConfigurationToDevice()
        {
            var data = BinaryStructure.Write(Configuration);
            HidConnector.Instance.WriteConfiguration(data, null);
        }

        public String DeviceName
        {
            get
            {
                return HidDeviceInfo.Get(Configuration.Info.ProductId).Name;
            }
        }

        public string MinimumBuildNumber
        {
            get
            {
                return MinimumSupportedBuildNumber.ToString();
            }
        }

        public string  HwVer
        {
            get
            {
                return (Configuration.Info.HardwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
            }
        }

        public string FwVer
        {
            get
            {
                return (Configuration.Info.FirmwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
            }
        }

        public string Build
        {
            get
            {
                return Configuration.Info.FirmwareBuild.ToString();
            }
        }

        
    }
}