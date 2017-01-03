
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
using static NToolbox.Models.ArcticFoxConfiguration;

namespace NToolbox.ViewModels
{



    public class ArcticFoxConfigurationViewModel
    {   
        private const ushort MaxPower = 2500;
        private const byte MaxBatteries = 3;
        private const int MinimumSupportedBuildNumber = 161223;
        private const int MaximumSupportedSettingsVersion = 5;

        public static ArcticFoxConfigurationViewModel Instance { get; } = new ArcticFoxConfigurationViewModel();

        public static Dictionary<String, byte> PreheatTypes
        {
            get
            {
                var preheatTypes = new Dictionary<String, byte>()
                {
                    { "Absolute (W)" , (byte)PreheatType.Watts},
                    { "Relative (%)" , (byte)PreheatType.Percents},                   
                    { "Curve" , (byte)PreheatType.Curve},
                };
                return preheatTypes;
            }
        }

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

            m_ProfileViewModels.Clear();
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

        public string HwVer
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

        public ProfileSelection SelectedProfile
        {
            get
            {
                return (ProfileSelection)Configuration.General.SelectedProfile;
            }
        }


        private Dictionary<ProfileSelection, ProfileViewModel> m_ProfileViewModels = new Dictionary<ProfileSelection, ProfileViewModel>();

        public ProfileViewModel GetProfileViewModel(ProfileSelection profile)
        {
            ProfileViewModel viewModel = null;
            if(!m_ProfileViewModels.TryGetValue(profile, out viewModel)){
                var index = Convert.ToInt32((byte)profile);
                viewModel = new ProfileViewModel(Configuration, Configuration.General.Profiles[index], index);
                m_ProfileViewModels.Add(profile, viewModel);
                viewModel.ReadConfiguration();
            }
            return viewModel;            
        }

    }

    public class ProfileViewModel
    {
        private ArcticFoxConfiguration m_configuration;
        private ArcticFoxConfiguration.Profile m_profile;
        private Int32 m_ProfileIndex;

        internal ProfileViewModel(ArcticFoxConfiguration configuration,   ArcticFoxConfiguration.Profile profile, Int32 profileIndex)
        {
            m_profile = profile;
            m_ProfileIndex = profileIndex;
            m_configuration = configuration;
        }

        public void ReadConfiguration()
        {
            ProfileName = m_profile.Name;
            Power = Convert.ToDecimal(m_profile.Power) / Convert.ToDecimal(10);
            PreHitPower = m_profile.PreheatPower;
            PreheatType = ArcticFoxConfigurationViewModel.PreheatTypes.Where( x => x.Value == (byte)m_profile.PreheatType).First().Key;
        }

        public String ProfileName { get; set; }

        public Decimal Power { get; set; }

        public Decimal MaxPower
        {
            get
            {
                return Convert.ToDecimal(m_configuration.Info.MaxPower) / Convert.ToDecimal(10);
            }
        }

        public Decimal PreHitPower { get; set; }
        
        public string PreheatType { get; set; }

    }

    public enum ProfileSelection
    {
        Profile1 = (byte)0x0,
        Profile2 = (byte)0x1,
        Profile3 = (byte)0x2,
        Profile4 = (byte)0x3,
        Profile5 = (byte)0x4,
        Profile6 = (byte)0x5,
        Profile7 = (byte)0x6,
        Profile8 = (byte)0x7,
    }
}