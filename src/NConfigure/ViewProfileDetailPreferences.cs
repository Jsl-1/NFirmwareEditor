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

            var pref = PreferenceScreen.FindPreference("prefs_profiledetail_isactive");
            pref.Enabled = !this.PreferenceManager.SharedPreferences.GetBoolean("prefs_profiledetail_isactive", false);
            
        }

        public override void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            if(key == "prefs_profiledetail_isactive")
            {
                var generalPreferences = Application.Context.GetSharedPreferences("general", FileCreationMode.Private);
                using (var editor = generalPreferences.Edit())
                {
                    editor.PutString("pref_general_selectedprofile", Tag);
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