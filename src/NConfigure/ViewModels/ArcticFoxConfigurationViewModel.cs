
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
using NCore.UI;

namespace NToolbox.ViewModels
{
    public class ArcticFoxConfigurationViewModel 
    {   
        private const ushort MaxPower = 2500;
        private const byte MaxBatteries = 3;
        private const int MinimumSupportedBuildNumber = 170103;
        private const int MaximumSupportedSettingsVersion = 6;

        private ArcticFoxConfiguration Configuration { get; set; }

        private ISharedPreferences Preference
        {
            get
            {
                return Application.Context.GetSharedPreferences("General", FileCreationMode.Private);
            }
        }

        public event EventHandler OnReload;

        public ArcticFoxConfigurationViewModel()
        {
            Configuration = BinaryStructure.Read<ArcticFoxConfiguration>(Properties.Resources.new_configuration);
            ReadConfiguration(false);

        }

        public void ReadConfigurationFromDevice()
        {
            ReadConfiguration(true);
        }

        public void ReadConfiguration(Boolean refreshFromDevice = false)
        {
            if (refreshFromDevice)
            {
                var data = HidConnector.Instance.ReadConfiguration();
                Configuration = BinaryStructure.Read<ArcticFoxConfiguration>(data);
            }

          

            var generalPreferences = Application.Context.GetSharedPreferences("general", FileCreationMode.Private);
            using (var editor = generalPreferences.Edit())
            {
                editor.PutInt(PreferenceKeys.prefs_info_maxpower, Configuration.Info.MaxPower);
                editor.PutString(PreferenceKeys.prefs_general_selectedprofile, String.Format("Profile{0}", Convert.ToInt32(Configuration.General.SelectedProfile) + 1));
                editor.Commit();
            }

            for (var i = 0; i < Configuration.General.Profiles.Length; i++)
            {
                var profile = Configuration.General.Profiles[i];
                var preferences = Application.Context.GetSharedPreferences(String.Format("Profile{0}", (i + 1)), FileCreationMode.Private);
                using (var editor = preferences.Edit())
                {
                    editor.PutBoolean("prefs_profiledetail_isactive", Configuration.General.SelectedProfile == Convert.ToByte(i));
                    editor.PutString(PreferenceKeys.prefs_general_profiles_name, profile.Name);

                    editor.PutString(PreferenceKeys.prefs_general_profiles_flags_material, profile.Flags.Material.ToString());
                    editor.PutString("pref_profiledetail_powermode", profile.Flags.Material == Material.VariWatt ? "0" : "1");

                    editor.PutBoolean(PreferenceKeys.prefs_general_profiles_flags_istemperaturedominant, profile.Flags.IsTemperatureDominant);
                    editor.PutString(PreferenceKeys.prefs_general_profiles_flags_iscelcius, profile.Flags.IsCelcius.ToString().ToLower());
                    editor.PutBoolean(PreferenceKeys.prefs_general_profiles_flags_isresistancelocked, profile.Flags.IsResistanceLocked);
                    editor.PutBoolean(PreferenceKeys.prefs_general_profiles_flags_isenabled, profile.Flags.IsEnabled);

                    editor.PutString(PreferenceKeys.prefs_general_profiles_preheattype, profile.PreheatType.ToString());
                    editor.PutString(PreferenceKeys.prefs_general_profiles_selectedcurve, ((byte)profile.SelectedCurve).ToString());
                    editor.PutInt(PreferenceKeys.prefs_general_profiles_preheattime, profile.PreheatTime);
                    editor.PutInt(PreferenceKeys.prefs_general_profiles_preheatdelay, profile.PreheatDelay);
                    editor.PutInt(PreferenceKeys.prefs_general_profiles_preheatpower, profile.PreheatPower);

                    editor.PutInt(PreferenceKeys.prefs_general_profiles_power, profile.Power);
                    editor.PutInt(PreferenceKeys.prefs_general_profiles_temperature, profile.Temperature);
                    editor.PutInt(PreferenceKeys.prefs_general_profiles_resistance, profile.Resistance);
                    editor.PutInt(PreferenceKeys.prefs_general_profiles_tcr, profile.TCR);

                    editor.Commit();
                }
            }

            if (OnReload != null)
                OnReload(this, EventArgs.Empty);
        }


        public void WriteConfigurationToDevice()
        {
            var generalPreferences = Application.Context.GetSharedPreferences("general", FileCreationMode.Private);

            Configuration.General.SelectedProfile = Convert.ToByte(Int32.Parse(generalPreferences.GetString(PreferenceKeys.prefs_general_selectedprofile, "0").LastOrDefault().ToString()) -1);

            for (var i = 0; i < Configuration.General.Profiles.Length; i++)
            {
                var profile = Configuration.General.Profiles[i];
                using (var preferences = Application.Context.GetSharedPreferences(String.Format("Profile{0}", (i + 1)), FileCreationMode.Private))
                {
                    profile.Name = preferences.GetString(PreferenceKeys.prefs_general_profiles_name, String.Format("Profile{0}", (i + 1)));

                    var powerMode = preferences.GetString("pref_profiledetail_powermode", "0");
                    if(powerMode == "0")
                    {
                        profile.Flags.Material = Material.VariWatt;
                    }
                    else
                    {
                        var materialString = preferences.GetString(PreferenceKeys.prefs_general_profiles_flags_material, "");
                        Material material;
                        if (Enum.TryParse<Material>(materialString, out material))
                        {
                            profile.Flags.Material = material;
                        }
                    }

                    profile.Flags.IsTemperatureDominant = preferences.GetBoolean(PreferenceKeys.prefs_general_profiles_flags_istemperaturedominant, false);
                    profile.Flags.IsCelcius = preferences.GetString(PreferenceKeys.prefs_general_profiles_flags_iscelcius, "false").ToString() == true.ToString().ToLower();
                    profile.Flags.IsResistanceLocked = preferences.GetBoolean(PreferenceKeys.prefs_general_profiles_flags_isresistancelocked, false);
                    profile.Flags.IsEnabled = preferences.GetBoolean(PreferenceKeys.prefs_general_profiles_flags_isenabled, true);

                    var preheatTypeString = preferences.GetString(PreferenceKeys.prefs_general_profiles_preheattype, "");
                    PreheatType preheatType;
                    if (Enum.TryParse<PreheatType>(preheatTypeString, out preheatType))
                    {
                        profile.PreheatType = preheatType;
                    }

                    var selectedCurveString = preferences.GetString(PreferenceKeys.prefs_general_profiles_selectedcurve, "");
                    Byte selectedCurve;
                    if (Byte.TryParse(selectedCurveString, out selectedCurve))
                    {
                        profile.SelectedCurve = selectedCurve;
                    }

                    var preHeatTime = preferences.GetInt(PreferenceKeys.prefs_general_profiles_preheattime, 0);
                    profile.PreheatTime = Convert.ToByte(preHeatTime );

                    var preHeatDelay= preferences.GetInt(PreferenceKeys.prefs_general_profiles_preheatdelay, 0);
                    profile.PreheatDelay = Convert.ToByte(preHeatDelay);

                    var preHeatpower= preferences.GetInt(PreferenceKeys.prefs_general_profiles_preheatpower, 0);
                    profile.PreheatPower = Convert.ToUInt16(preHeatpower);

                    var power = preferences.GetInt(PreferenceKeys.prefs_general_profiles_power, 0);
                    profile.Power = Convert.ToUInt16(power);

                    var temperature = preferences.GetInt(PreferenceKeys.prefs_general_profiles_temperature, 0);
                    profile.Temperature = Convert.ToUInt16(temperature);

                    var resistance = preferences.GetInt(PreferenceKeys.prefs_general_profiles_resistance, 0);
                    profile.Resistance = Convert.ToUInt16(resistance);

                    var tcr = preferences.GetInt(PreferenceKeys.prefs_general_profiles_tcr, 0);
                    profile.TCR = Convert.ToUInt16(tcr);
                }
            }

            var data = BinaryStructure.Write(Configuration);
            HidConnector.Instance.WriteConfiguration(data, null);
        }

        public String DeviceName
        {
            get
            {
                return Preference.GetString(nameof(Resource.String.pref_device_name), String.Empty);              
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

        public IntPtr Handle
        {
            get
            {
                throw new NotImplementedException();
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