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
using NToolbox.UI;
using Android.Preferences;
using NToolbox;

namespace NToolbox
{
    public class ViewProfileDetailPreferences : PreferenceFragmentBase
    {
        public const Int32 XmlPreferenceId = Resource.Xml.preferences_profile_detail;
        
        public override int PreferenceId
        {
            get
            {
                return XmlPreferenceId;
            }
        }


        public override void OnCreate(Bundle savedInstanceState)
        {
           

            base.OnCreate(savedInstanceState);

           

        }

        public override void RefreshUi()
        {
            base.RefreshUi();

            var mainActictivity = (MainActivity)this.Activity;
            var viewModel = mainActictivity.ViewModel;

            var profileIsActivePreference = PreferenceScreen.FindPreference(PreferenceKeys.prefs_profiledetail_isactive);
            profileIsActivePreference.Enabled = !this.PreferenceManager.SharedPreferences.GetBoolean(PreferenceKeys.prefs_profiledetail_isactive, false);
            
            var profilePowerPreference = PreferenceScreen.FindPreference(PreferenceKeys.prefs_general_profiles_power);
            var test = profilePowerPreference.GetType();

            

        }

        public override void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            if(key == "prefs_profiledetail_isactive")
            {
                var generalPreferences = Application.Context.GetSharedPreferences("general", FileCreationMode.Private);
                using (var editor = generalPreferences.Edit())
                {
                    editor.PutString(PreferenceKeys.prefs_general_selectedprofile, Tag);
                    editor.Commit();
                }
                for(var i=1; i<=8; i++)
                {
                    var profileName = String.Format("Profile{0}", i);
                    var profilePreferences = Application.Context.GetSharedPreferences(profileName, FileCreationMode.Private);
                    if (profileName != Tag)
                    {
                        using (var editor = profilePreferences.Edit())
                        {
                            editor.PutBoolean("prefs_profiledetail_isactive", false);
                            editor.Commit();
                        }
                    }
                }

                PreferenceScreen.FindPreference("prefs_profiledetail_isactive").Enabled = false;

            }
            base.OnSharedPreferenceChanged(sharedPreferences, key);
        }

    }
}