
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

        private Activity m_Activity;

        private ArcticFoxConfiguration Configuration { get; set; }

        private ISharedPreferences Preference
        {
            get
            {
                return Application.Context.GetSharedPreferences("General", FileCreationMode.Private);
            }
        }

        public event EventHandler OnReload;

        public ArcticFoxConfigurationViewModel(Activity activity)
        {
            m_Activity = activity;
            //Configuration = new ArcticFoxConfiguration();
            Configuration = BinaryStructure.Read<ArcticFoxConfiguration>(Properties.Resources.new_configuration);
        }

        public void ReadConfigurationFromDevice()
        {
            var data = HidConnector.Instance.ReadConfiguration();
            Configuration = BinaryStructure.Read<ArcticFoxConfiguration>(data);

            var generalPreferences = Application.Context.GetSharedPreferences("general", FileCreationMode.Private);
            using (var editor = generalPreferences.Edit())
            {
                editor.PutString("pref_general_selectedprofile", String.Format("Profile{0}", Convert.ToInt32(Configuration.General.SelectedProfile) + 1));

                editor.Commit();
            }

            for (var i = 0; i < Configuration.General.Profiles.Length; i++)
            {
                var profile = Configuration.General.Profiles[i];
                var preferences = m_Activity.GetSharedPreferences(String.Format("Profile{0}", (i + 1)), FileCreationMode.Private);
                using (var editor = preferences.Edit())
                {
                    editor.PutBoolean("prefs_profiledetail_isactive", Configuration.General.SelectedProfile == Convert.ToByte(i));
                    editor.PutString("pref_profiledetail_name", profile.Name);

                    editor.PutString("pref_profiledetail_material", profile.Flags.Material.ToString());
                    editor.PutString("pref_profiledetail_powermode", profile.Flags.Material == Material.VariWatt ? "0" : "1");

                    editor.PutBoolean("pref_profiledetail_istemperaturedominant", profile.Flags.IsTemperatureDominant);
                    editor.PutString("pref_profiledetail_iscelcius", profile.Flags.IsCelcius.ToString().ToLower());
                    editor.PutBoolean("pref_profiledetail_isresistancelocked", profile.Flags.IsResistanceLocked);
                    editor.PutBoolean("pref_profiledetail_isenabled", profile.Flags.IsEnabled);

                    editor.PutString("pref_profiledetail_preheattype", profile.PreheatType.ToString());
                    editor.PutString("pref_profiledetail_selectedcurve", ((byte)profile.SelectedCurve).ToString());
                    editor.PutString("pref_profiledetail_preheattime", (profile.PreheatTime / 100f).ToString());
                    editor.PutString("pref_profiledetail_preheatdelay", (profile.PreheatDelay / 10f).ToString());
                    editor.PutString("pref_profiledetail_preheatpower", (profile.PreheatPower / 10f).ToString());

                    editor.PutString("pref_profiledetail_power", (profile.Power / 10f).ToString());
                    editor.PutString("pref_profiledetail_temperature", profile.Temperature.ToString());
                    editor.PutString("pref_profiledetail_resistance", (profile.Resistance / 1000f).ToString());
                    editor.PutString("pref_profiledetail_tcr", profile.TCR.ToString());


                    editor.Commit();
                }
            }

            if (OnReload != null)
                OnReload(this, EventArgs.Empty);
        }


        public void WriteConfigurationToDevice()
        {
            var generalPreferences = Application.Context.GetSharedPreferences("general", FileCreationMode.Private);

            Configuration.General.SelectedProfile = Convert.ToByte(Int32.Parse(generalPreferences.GetString("pref_general_selectedprofile", "0").LastOrDefault().ToString()) -1);

            for (var i = 0; i < Configuration.General.Profiles.Length; i++)
            {
                var profile = Configuration.General.Profiles[i];
                using (var preferences = Application.Context.GetSharedPreferences(String.Format("Profile{0}", (i + 1)), FileCreationMode.Private))
                {
                    profile.Name = preferences.GetString("pref_profiledetail_name", String.Format("Profile{0}", (i + 1)));

                    var powerMode = preferences.GetString("pref_profiledetail_powermode", "0");
                    if(powerMode == "0")
                    {
                        profile.Flags.Material = Material.VariWatt;
                    }
                    else
                    {
                        var materialString = preferences.GetString("pref_profiledetail_name", "");
                        Material material;
                        if (Enum.TryParse<Material>(materialString, out material))
                        {
                            profile.Flags.Material = material;
                        }
                    }

                    profile.Flags.IsTemperatureDominant = preferences.GetBoolean("pref_profiledetail_istemperaturedominant", false);
                    profile.Flags.IsCelcius = preferences.GetString("pref_profiledetail_iscelcius", "false").ToString() == true.ToString().ToLower();
                    profile.Flags.IsResistanceLocked = preferences.GetBoolean("ref_profiledetail_isresistancelocked", false);
                    profile.Flags.IsEnabled = preferences.GetBoolean("pref_profiledetail_isenabled", true);

                    var preheatTypeString = preferences.GetString("pref_profiledetail_preheattype", "");
                    PreheatType preheatType;
                    if (Enum.TryParse<PreheatType>(preheatTypeString, out preheatType))
                    {
                        profile.PreheatType = preheatType;
                    }

                    var selectedCurveString = preferences.GetString("pref_profiledetail_selectedcurve", "");
                    Byte selectedCurve;
                    if (Byte.TryParse(selectedCurveString, out selectedCurve))
                    {
                        profile.SelectedCurve = selectedCurve;
                    }

                    var preHeatTimeString = preferences.GetString("pref_profiledetail_preheattime", "");
                    Single preHeatTime;
                    if (Single.TryParse(preHeatTimeString, out preHeatTime))
                    {
                        profile.PreheatTime = Convert.ToByte(Convert.ToInt32(preHeatTime * 100f) );
                    }

                    var preHeatDelayString = preferences.GetString("pref_profiledetail_preheatdelay", "");
                    Single preHeatDelay;
                    if (Single.TryParse(preHeatDelayString, out preHeatDelay))
                    {
                        profile.PreheatDelay = Convert.ToByte(Convert.ToInt32(preHeatDelay * 10f));
                    }

                    var preHeatpowerString = preferences.GetString("pref_profiledetail_preheatpower", "");
                    Single preHeatpower;
                    if (Single.TryParse(preHeatpowerString, out preHeatpower))
                    {
                        profile.PreheatPower = Convert.ToUInt16(Convert.ToInt32(preHeatpower * 10f));
                    }

                    var powerString = preferences.GetString("pref_profiledetail_power", "");
                    Single power;
                    if (Single.TryParse(powerString, out power))
                    {
                        profile.Power = Convert.ToUInt16(Convert.ToInt32(power * 10f));
                    }

                    var temperatureString = preferences.GetString("pref_profiledetail_temperature", "");
                    UInt16 temperature;
                    if (UInt16.TryParse(temperatureString, out temperature))
                    {
                        profile.Temperature = temperature;
                    }

                    var resistanceString = preferences.GetString("pref_profiledetail_resistance", "");
                    Single resistance;
                    if (Single.TryParse(resistanceString, out resistance))
                    {
                        profile.Resistance = Convert.ToUInt16(resistance * 1000f);
                    }

                    var tcrString = preferences.GetString("pref_profiledetail_tcr", "");
                    UInt16 tcr;
                    if (UInt16.TryParse(tcrString, out tcr))
                    {
                        profile.TCR = tcr;
                    }


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